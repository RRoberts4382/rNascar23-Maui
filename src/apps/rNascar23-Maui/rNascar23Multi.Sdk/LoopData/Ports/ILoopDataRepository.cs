using rNascar23Multi.Sdk.Common;
using rNascar23Multi.Sdk.LoopData.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23Multi.Sdk.LoopData.Ports
{
    public interface ILoopDataRepository
    {
        Task<EventLoopData> GetLoopDataAsync(SeriesTypes seriesId, int raceId, int? year = null, CancellationToken cancellationToken = default);
    }
}
