using Microsoft.Extensions.DependencyInjection;
using rNascar23Multi.Sdk.LapTimes.Ports;
using rNascar23Multi.Sdk.Service.LapTimes.Adapters;

namespace rNascar23Multi.Sdk.Service.LapTimes
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLapTimes(this IServiceCollection services)
        {
            services
                .AddTransient<IMoversFallersService, MoversFallersService>()
                .AddTransient<ILapTimesRepository, LapTimesRepository>()
                .AddTransient<ILapAveragesRepository, LapAveragesRepository>();

            return services;
        }
    }
}
