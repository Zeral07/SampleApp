using AutoMapper;
using SampleApp.Models;
using SampleApp.ViewModels;

namespace SampleApp.AutoMapper
{
    public class UsersLoginProfile : Profile
    {
        public UsersLoginProfile()
        {
            CreateMap<UsersLogin, UsersLoginDto>();
            CreateMap<UsersLoginDto, UsersLogin>();
        }
    }
}
