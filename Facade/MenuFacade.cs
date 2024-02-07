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

        public async Task<MenuDto> Create(Menu obj)
        {
            _context.Menus.Add(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<MenuDto>(obj) ?? new();
        }

        public async Task<MenuDto> Delete(Menu obj)
        {
            obj.RowStatus = (int)DBRowStatus.NotActive;
            _context.Menus.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<MenuDto>(obj) ?? new();
        }

        public async Task<List<MenuDto>> GetAll()
        {
            return _mapper.Map<List<MenuDto>>(await _context.Menus.ToListAsync()) ?? [];
        }

        public async Task<MenuDto> GetByID(int id)
        {
            return _mapper.Map<MenuDto>(await _context.Menus.Where(w => w.Id == id).SingleOrDefaultAsync()) ?? new();
        }

        public async Task<List<MenuDto>> GetByParameter(string condition, List<object> parameters)
        {
            return _mapper.Map<List<MenuDto>>(await _context.Menus.Where(condition, parameters.ToArray()).ToListAsync()) ?? [];
        }

        public async Task<ResultResponse<MenuDto>> GetByParameterWithPaging(string condition, List<object> parameters, int pageCount, int pageSize)
        {
            var list = _mapper.Map<List<MenuDto>>(await _context.Menus.Where(condition, parameters.ToArray()).ToListAsync()) ?? [];
            ResultResponse<MenuDto> result = new()
            {
                Result = list.Skip(pageCount == 1 ? 0 : pageCount * pageSize - pageSize).Take(pageSize).ToList(),
                Total = list.Count
            };
            return result;
        }

        public async Task<MenuDto> Update(Menu obj)
        {
            _context.Menus.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<MenuDto>(obj) ?? new();
        }
    }
}
