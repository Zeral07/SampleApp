using Microsoft.EntityFrameworkCore;
using SampleApp.Enums;
using SampleApp.Helpers;
using SampleApp.Interface;
using SampleApp.Models;
using SampleApp.ViewModels;
using System.Linq.Dynamic.Core;

namespace SampleApp.Facade
{
    public class UsersLoginFacade(SampleDbContext context, IHttpContextAccessor httpContextAccessor) : IUsersLoginFacade
    {
        private readonly SampleDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<UsersLogin> Create(UsersLogin obj)
        {
            if (_httpContextAccessor.HttpContext != null)
                obj.CreatedBy = ServiceCoreHelper.CurrentUser(_httpContextAccessor.HttpContext);
            else
                obj.CreatedBy = "System";
            obj.CreatedTime = DateTime.Now;
            _context.UsersLogins.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _context.UsersLogins.Where(w => w.Id == id && w.RowStatus == (int)DBRowStatus.Active).SingleOrDefaultAsync();
            if (obj != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                    obj.LastUpdatedBy = ServiceCoreHelper.CurrentUser(_httpContextAccessor.HttpContext);
                else
                    obj.LastUpdatedBy = "System";
                obj.LastUpdatedTime = DateTime.Now;
                obj.RowStatus = (int)DBRowStatus.NotActive;
                _context.UsersLogins.Update(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<UsersLogin>> GetAll()
        {
            return await _context.UsersLogins.Where(w => w.RowStatus == (int)DBRowStatus.Active).ToListAsync();
        }

        public async Task<UsersLogin?> GetByID(int id)
        {
            return await _context.UsersLogins.Where(w => w.Id == id && w.RowStatus == (int)DBRowStatus.Active).SingleOrDefaultAsync();
        }

        public async Task<List<UsersLogin>> GetByParameter(string condition, List<object> parameters)
        {
            return await _context.UsersLogins.Where(condition, parameters.ToArray()).ToListAsync();
        }

        public async Task<PaginationFilter<UsersLogin>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize)
        {
            var list = await _context.UsersLogins.Where(condition, parameters.ToArray()).ToListAsync();
            PaginationFilter<UsersLogin> result = new()
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

        public async Task Update(UsersLogin obj)
        {
            if (_httpContextAccessor.HttpContext != null)
                obj.LastUpdatedBy = ServiceCoreHelper.CurrentUser(_httpContextAccessor.HttpContext);
            else
                obj.LastUpdatedBy = "System";
            obj.LastUpdatedTime = DateTime.Now;
            _context.UsersLogins.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
