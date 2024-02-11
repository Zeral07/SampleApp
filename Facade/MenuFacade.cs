﻿using Microsoft.EntityFrameworkCore;
using SampleApp.Enums;
using SampleApp.Helpers;
using SampleApp.Interface;
using SampleApp.Models;
using SampleApp.ViewModels;
using System.Linq.Dynamic.Core;

namespace SampleApp.Facade
{
    public class MenuFacade(SampleDbContext context, IHttpContextAccessor httpContextAccessor) : IMenuFacade
    {
        private readonly SampleDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Menu> Create(Menu obj)
        {
            if (_httpContextAccessor.HttpContext != null)
                obj.CreatedBy = ServiceCoreHelper.CurrentUser(_httpContextAccessor.HttpContext);
            else
                obj.CreatedBy = "System";
            obj.CreatedTime = DateTime.Now;
            _context.Menus.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _context.Menus.Where(w => w.Id == id && w.RowStatus == (int)DBRowStatus.Active).SingleOrDefaultAsync();
            if (obj != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                    obj.LastUpdatedBy = ServiceCoreHelper.CurrentUser(_httpContextAccessor.HttpContext);
                else
                    obj.LastUpdatedBy = "System";
                obj.LastUpdatedTime = DateTime.Now;
                obj.RowStatus = (int)DBRowStatus.NotActive;
                _context.Menus.Update(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Menu>> GetAll()
        {
            return await _context.Menus.Where(w => w.RowStatus == (int)DBRowStatus.Active).ToListAsync();
        }

        public async Task<Menu?> GetByID(int id)
        {
            return await _context.Menus.Where(w => w.Id == id && w.RowStatus == (int)DBRowStatus.Active).SingleOrDefaultAsync();
        }

        public async Task<List<Menu>> GetByUserID(int userId)
        {
            return await _context.Menus.FromSql($"dbo.sp_GetMenusByUserID @UserID={userId}").ToListAsync();
        }

        public async Task<List<Menu>> GetByParameter(string condition, List<object> parameters)
        {
            return await _context.Menus.Where(condition, parameters.ToArray()).ToListAsync();
        }

        public async Task<PaginationFilter<Menu>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize)
        {
            var list = await _context.Menus.Where(condition, parameters.ToArray()).ToListAsync();
            PaginationFilter<Menu> result = new()
            {
                Result = list.Skip(pageCount == 1 ? 0 : pageCount * pageSize - pageSize).Take(pageSize).ToList(),
                Total = list.Count
            };

            if (result.Result.Count == 0 && result.Total > 0)
            {
                result.Result = list.Take(pageSize).ToList();
                result.PageNumber = 1;
            }
            else
                result.PageNumber = pageCount;

            return result;
        }

        public async Task Update(Menu obj)
        {
            if (_httpContextAccessor.HttpContext != null)
                obj.LastUpdatedBy = ServiceCoreHelper.CurrentUser(_httpContextAccessor.HttpContext);
            else
                obj.LastUpdatedBy = "System";
            obj.LastUpdatedTime = DateTime.Now;
            _context.Menus.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
