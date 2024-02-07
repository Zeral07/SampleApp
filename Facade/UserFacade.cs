using Microsoft.EntityFrameworkCore;
using SampleApp.Enums;
using SampleApp.Interface;
using SampleApp.Models;
using SampleApp.ViewModels;
using System.Linq.Dynamic.Core;

namespace SampleApp.Facade
{
    public class UserFacade(SampleDbContext context, IHttpContextAccessor httpContextAccessor) : IUserFacade
    {
        private readonly SampleDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<User> Create(User obj)
        {
            obj.CreatedBy = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "System";
            obj.CreatedTime = DateTime.Now;
            _context.Users.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<User> Delete(User obj)
        {
            obj.LastUpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            obj.LastUpdatedTime = DateTime.Now;
            obj.RowStatus = (int)DBRowStatus.NotActive;
            _context.Users.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByID(int id)
        {
            return await _context.Users.Where(w => w.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<User>> GetByParameter(string condition, List<object> parameters)
        {
            return await _context.Users.Where(condition, parameters.ToArray()).ToListAsync();
        }

        public async Task<ResultResponse<User>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize)
        {
            var list = await _context.Users.Where(condition, parameters.ToArray()).ToListAsync();
            ResultResponse<User> result = new ResultResponse<User>
            {
                Result = list.Skip(pageCount == 1 ? 0 : pageCount * pageSize - pageSize).Take(pageSize).ToList(),
                Total = list.Count
            };
            return result;
        }

        public async Task<User> Update(User obj)
        {
            obj.LastUpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            obj.LastUpdatedTime = DateTime.Now;
            _context.Users.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
