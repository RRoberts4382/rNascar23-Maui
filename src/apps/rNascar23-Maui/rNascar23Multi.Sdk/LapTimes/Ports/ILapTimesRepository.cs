using rNascar23Multi.Sdk.Common;
using rNascar23Multi.Sdk.LapTimes.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23Multi.Sdk.LapTimes.Ports
{
    public interface ILapTimesRepository
    {
        Task<LapTimeData> GetLapTimeDataAsync(
            SeriesTypes seriesId,
            int raceId,
            int? year = null,
            CancellationToken cancellationToken = default);

        Task<LapTimeData> GetLapTimeDataAsync(
            SeriesTypes seriesId,
            int raceId,
            int driverId,
            int? year = null,
            CancellationToken cancellationToken = default);
    }
}
