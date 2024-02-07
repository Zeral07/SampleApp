using Microsoft.EntityFrameworkCore;
using SampleApp.Enums;
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
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity != null)
                obj.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            else
                obj.CreatedBy = "System";
            obj.CreatedTime = DateTime.Now;
            _context.Menus.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<Menu> Delete(Menu obj)
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity != null)
                obj.LastUpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            else
                obj.LastUpdatedBy = "System";
            obj.LastUpdatedTime = DateTime.Now;
            obj.RowStatus = (int)DBRowStatus.NotActive;
            _context.Menus.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<Menu>> GetAll()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu?> GetByID(int id)
        {
            return await _context.Menus.Where(w => w.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<Menu>> GetByParameter(string condition, List<object> parameters)
        {
            return await _context.Menus.Where(condition, parameters.ToArray()).ToListAsync();
        }

        public async Task<ResultResponse<Menu>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize)
        {
            var list = await _context.Menus.Where(condition, parameters.ToArray()).ToListAsync();
            ResultResponse<Menu> result = new()
            {
                Result = list.Skip(pageCount == 1 ? 0 : pageCount * pageSize - pageSize).Take(pageSize).ToList(),
                Total = list.Count
            };
            return result;
        }

        public async Task<Menu> Update(Menu obj)
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity != null)
                obj.LastUpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            else
                obj.LastUpdatedBy = "System";
            obj.LastUpdatedTime = DateTime.Now;
            _context.Menus.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
