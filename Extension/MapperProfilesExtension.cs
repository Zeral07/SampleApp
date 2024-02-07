using SampleApp.AutoMapper;

namespace SampleApp.Extension
{
    public static class MapperProfilesExtension
    {
        public static IServiceCollection AddMapperProfiles(
            this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserProfile),
                typeof(MenuProfile)
            );

            return services;
        }
    }
}
