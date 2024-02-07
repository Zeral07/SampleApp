using Microsoft.EntityFrameworkCore;
using SampleApp.Enums;
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
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity != null)
                obj.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            else
                obj.CreatedBy = "System";
            obj.CreatedTime = DateTime.Now;
            _context.UsersLogins.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<UsersLogin> Delete(UsersLogin obj)
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity != null)
                obj.LastUpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            else
                obj.LastUpdatedBy = "System";
            obj.LastUpdatedTime = DateTime.Now;
            obj.RowStatus = (int)DBRowStatus.NotActive;
            _context.UsersLogins.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<UsersLogin>> GetAll()
        {
            return await _context.UsersLogins.ToListAsync();
        }

        public async Task<UsersLogin?> GetByID(int id)
        {
            return await _context.UsersLogins.Where(w => w.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<UsersLogin>> GetByParameter(string condition, List<object> parameters)
        {
            return await _context.UsersLogins.Where(condition, parameters.ToArray()).ToListAsync();
        }

        public async Task<ResultResponse<UsersLogin>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize)
        {
            var list = await _context.UsersLogins.Where(condition, parameters.ToArray()).ToListAsync();
            ResultResponse<UsersLogin> result = new()
            {
                Result = list.Skip(pageCount == 1 ? 0 : pageCount * pageSize - pageSize).Take(pageSize).ToList(),
                Total = list.Count
            };
            return result;
        }

        public async Task<UsersLogin> Update(UsersLogin obj)
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity != null)
                obj.LastUpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            else
                obj.LastUpdatedBy = "System";
            obj.LastUpdatedTime = DateTime.Now;
            _context.UsersLogins.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
