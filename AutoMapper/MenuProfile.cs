using AutoMapper;
using SampleApp.Models;
using SampleApp.ViewModels;

namespace SampleApp.AutoMapper
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>();
        }
    }
}
