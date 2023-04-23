using rNascar23Multi.Sdk.Common;
using rNascar23Multi.Sdk.Points.Models;

namespace rNascar23Multi.Sdk.Points.Ports
{
    public interface IPointsRepository
    {
        Task<IEnumerable<DriverPoints>> GetDriverPointsAsync(
            SeriesTypes seriesId, 
            int raceId,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<StagePointsDetails>> GetStagePointsAsync(
            SeriesTypes seriesId, 
            int raceId,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
