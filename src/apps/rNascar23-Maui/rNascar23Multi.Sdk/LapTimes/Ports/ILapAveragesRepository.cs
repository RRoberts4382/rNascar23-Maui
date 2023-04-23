using rNascar23Multi.Sdk.Common;
using rNascar23Multi.Sdk.LapTimes.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23Multi.Sdk.LapTimes.Ports
{
    public interface ILapAveragesRepository
    {
        Task<IEnumerable<LapAverages>> GetLapAveragesAsync(
            SeriesTypes seriesId,
            int raceId,
            int? year = null,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);
    }
}
