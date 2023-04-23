using Microsoft.Extensions.DependencyInjection;
using rNascar23Multi.Sdk.PitStops.Ports;
using rNascar23Multi.Sdk.Service.PitStops.Adapters;

namespace rNascar23Multi.Sdk.Service.PitStops
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPitStops(this IServiceCollection services)
        {
            services
                .AddTransient<IPitStopsRepository, PitStopsRepository>();

            return services;
        }
    }
}