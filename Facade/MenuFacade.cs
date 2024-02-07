using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleApp.Enums;
using SampleApp.Interface;
using SampleApp.Models;
using SampleApp.ViewModels;
using System.Linq.Dynamic.Core;

namespace SampleApp.Facade
{
    public class MenuFacade(SampleDbContext context, IMapper mapper) : IMenuFacade
    {
        private readonly SampleDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Menu> Create(Menu obj)
        {
            _context.Menus.Add(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<Menu>(obj) ?? new();
        }

        public async Task<Menu> Delete(Menu obj)
        {
            obj.RowStatus = (int)DBRowStatus.NotActive;
            _context.Menus.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<Menu>(obj) ?? new();
        }

        public async Task<List<Menu>> GetAll()
        {
            return _mapper.Map<List<Menu>>(await _context.Menus.ToListAsync()) ?? [];
        }

        public async Task<Menu> GetByID(int id)
        {
            return _mapper.Map<Menu>(await _context.Menus.Where(w => w.Id == id).SingleOrDefaultAsync()) ?? new();
        }

        public async Task<List<Menu>> GetByParameter(string condition, List<object> parameters)
        {
            return _mapper.Map<List<Menu>>(await _context.Menus.Where(condition, parameters.ToArray()).ToListAsync()) ?? [];
        }

        public async Task<ResultResponse<Menu>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize)
        {
            var list = _mapper.Map<List<Menu>>(await _context.Menus.Where(condition, parameters.ToArray()).ToListAsync()) ?? [];
            ResultResponse<Menu> result = new()
            {
                Result = list.Skip(pageCount == 1 ? 0 : pageCount * pageSize - pageSize).Take(pageSize).ToList(),
                Total = list.Count
            };
            return result;
        }

        public async Task<Menu> Update(Menu obj)
        {
            _context.Menus.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<Menu>(obj) ?? new();
        }
    }
}
