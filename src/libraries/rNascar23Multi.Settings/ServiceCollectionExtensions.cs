using rNascar23Multi.Settings.Adapters;
using rNascar23Multi.Settings.Ports;

namespace rNascar23Multi.Settings
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services)
        {
            services
               .AddTransient<ISettingsRepository, SettingsRepository>();

            return services;
        }
    }
}
