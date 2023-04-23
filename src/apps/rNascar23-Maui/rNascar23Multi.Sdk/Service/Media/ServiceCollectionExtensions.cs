using rNascar23Multi.Sdk.Media.Ports;

namespace rNascar23Multi.Sdk.Service.Media
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMedia(this IServiceCollection services)
        {
            services
                .AddSingleton<IMediaRepository, MediaRepository>();

            return services;
        }
    }
}
