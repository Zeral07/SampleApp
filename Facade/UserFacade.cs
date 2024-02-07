using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleApp.Enums;
using SampleApp.Interface;
using SampleApp.Models;
using SampleApp.ViewModels;
using System.Linq.Dynamic.Core;

namespace SampleApp.Facade
{
    public class UserFacade(SampleDbContext context, IMapper mapper) : IUserFacade
    {
        private readonly SampleDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<User> Create(User obj)
        {
            _context.Users.Add(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<User>(obj) ?? new();
        }

        public async Task<User> Delete(User obj)
        {
            obj.RowStatus = (int)DBRowStatus.NotActive;
            _context.Users.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<User>(obj) ?? new();
        }

        public async Task<List<User>> GetAll()
        {
            return _mapper.Map<List<User>>(await _context.Users.ToListAsync()) ?? [];
        }

        public async Task<User> GetByID(int id)
        {
            return _mapper.Map<User>(await _context.Users.Where(w => w.Id == id).SingleOrDefaultAsync()) ?? new();
        }

        public async Task<List<User>> GetByParameter(string condition, List<object> parameters)
        {
            return _mapper.Map<List<User>>(await _context.Users.Where(condition, parameters.ToArray()).ToListAsync()) ?? [];
        }

        public async Task<ResultResponse<User>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize)
        {
            var list = _mapper.Map<List<User>>(await _context.Users.Where(condition, parameters.ToArray()).ToListAsync()) ?? [];
            ResultResponse<User> result = new ResultResponse<User>
            {
                Result = list.Skip(pageCount == 1 ? 0 : pageCount * pageSize - pageSize).Take(pageSize).ToList(),
                Total = list.Count
            };
            return result;
        }

        public async Task<User> Update(User obj)
        {
            _context.Users.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<User>(obj) ?? new();
        }
    }
}
