using rNascar23Multi.Sdk.Common;
using rNascar23Multi.Sdk.LiveFeeds.Models;

namespace rNascar23Multi.Sdk.LiveFeeds.Ports
{
    public interface IKeyMomentsRepository
    {
        Task<IEnumerable<KeyMoment>> GetKeyMomentsAsync(
            SeriesTypes seriesId, 
            int raceId, 
            int? year = null,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
