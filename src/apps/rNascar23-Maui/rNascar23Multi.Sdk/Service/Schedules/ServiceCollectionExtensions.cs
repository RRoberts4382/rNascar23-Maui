using Microsoft.Extensions.DependencyInjection;
using rNascar23Multi.Sdk.Schedules.Ports;
using rNascar23Multi.Sdk.Service.Schedules.Adapters;

namespace rNascar23Multi.Sdk.Service.Schedules
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSchedules(this IServiceCollection services)
        {
            services
                .AddTransient<ISchedulesRepository, SchedulesRepository>();

            return services;
        }
    }
}
