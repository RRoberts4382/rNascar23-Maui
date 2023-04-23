using Microsoft.Extensions.DependencyInjection;
using rNascar23Multi.Sdk.Points.Ports;
using rNascar23Multi.Sdk.Service.Points.Adapters;

namespace rNascar23Multi.Sdk.Service.Points
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPoints(this IServiceCollection services)
        {
            services
                .AddTransient<IPointsRepository, PointsRepository>();

            return services;
        }
    }
}
