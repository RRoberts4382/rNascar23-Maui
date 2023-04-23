using rNascar23Multi.Sdk.Schedules.Models;

namespace rNascar23Multi.Sdk.Schedules.Ports
{
    public interface ISchedulesRepository
    {
        Task<SeriesSchedules> GetRaceListAsync(
            int? year = null, 
            CancellationToken cancellationToken=default);
    }
}
