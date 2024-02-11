using AutoMapper;
using SampleApp.Models;
using SampleApp.Parameters;
using SampleApp.ViewModels;

namespace SampleApp.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, UserParameter>();
        }
    }
}
