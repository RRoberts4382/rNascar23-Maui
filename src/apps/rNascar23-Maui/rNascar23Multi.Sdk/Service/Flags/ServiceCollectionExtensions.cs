using rNascar23Multi.Sdk.Flags.Ports;
using rNascar23Multi.Sdk.Service.Flags.Adapters;

namespace rNascar23Multi.Sdk.Service.Flags
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlagState(this IServiceCollection services)
        {
            services
                .AddTransient<IFlagStateRepository, FlagStateRepository>();

            return services;
        }
    }
}
