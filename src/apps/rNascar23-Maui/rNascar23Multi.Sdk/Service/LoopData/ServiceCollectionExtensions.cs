using rNascar23Multi.Sdk.LoopData.Ports;
using rNascar23Multi.Sdk.Service.LoopData.Adapters;
using rNascar23Multi.Sdk.Service.Schedules;

namespace rNascar23Multi.Sdk.Service.LoopData
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoopData(this IServiceCollection services)
        {
            services
                .AddSchedules()
                .AddTransient<IDriverInfoRepository, DriverInfoRepository>()
                .AddTransient<ILoopDataRepository, LoopDataRepository>();

            return services;
        }
    }
}
