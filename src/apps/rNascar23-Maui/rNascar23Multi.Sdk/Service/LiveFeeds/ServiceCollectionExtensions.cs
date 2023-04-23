using Microsoft.Extensions.DependencyInjection;
using rNascar23Multi.Sdk.Data.LiveFeeds.Ports;
using rNascar23Multi.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Sdk.Service.LiveFeeds.Adapters;

namespace rNascar23Multi.Sdk.Service.LiveFeeds
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiveFeed(this IServiceCollection services)
        {
            services
                .AddTransient<IKeyMomentsRepository, KeyMomentsRepository>()
                .AddTransient<IWeekendFeedRepository, WeekendFeedRepository>()
                .AddTransient<ILiveFeedRepository, LiveFeedRepository>();

            return services;
        }
    }
}
