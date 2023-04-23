using rNascar23Multi.Sdk.Common;
using rNascar23Multi.Sdk.LiveFeeds.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23Multi.Sdk.LiveFeeds.Ports
{
    public interface IWeekendFeedRepository
    {
        Task<WeekendFeed> GetWeekendFeedAsync(SeriesTypes seriesId, int raceId, int? year = null, CancellationToken cancellationToken = default);
    }
}
