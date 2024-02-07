using SampleApp.Facade;
using SampleApp.Interface;

namespace SampleApp.Extension
{
    public static class DIExtension
    {
        public static IServiceCollection AddDIGroup(
            this IServiceCollection services)
        {
            services.AddScoped<IUserFacade, UserFacade>();
            services.AddScoped<IUsersLoginFacade, UsersLoginFacade>();
            services.AddScoped<IMenuFacade, MenuFacade>();

            return services;
        }
    }
}
