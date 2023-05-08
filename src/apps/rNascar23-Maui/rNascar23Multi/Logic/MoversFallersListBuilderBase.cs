using rNascar23.Sdk.Common;
using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal abstract class MoversFallersListBuilderBase : ListBuilder<DriverValueModel>
    {
        private ILapTimesRepository _lapTimeRepository;
        private IMoversFallersService _moversFallersService;

        protected MoversFallersListBuilderBase(ILapTimesRepository lapTimeRepository, IMoversFallersService moversFallersService)
        {
            _lapTimeRepository = lapTimeRepository ?? throw new ArgumentNullException(nameof(lapTimeRepository));
            _moversFallersService = moversFallersService ?? throw new ArgumentNullException(nameof(moversFallersService));
        }

        public override async Task UpdateDataAsync(ObservableCollection<DriverValueModel> models, int? seriesId, int? raceId)
        {
            if (!seriesId.HasValue)
                throw new ArgumentNullException(nameof(seriesId));

            if (!raceId.HasValue)
                throw new ArgumentNullException(nameof(raceId));

            var lapTimeData = await _lapTimeRepository.GetLapTimeDataAsync((SeriesTypes)seriesId.Value, raceId.Value);

            var changes = _moversFallersService.GetDriverPositionChanges(lapTimeData);

            UpdateData(models, lapTimeData, changes);
        }

        internal abstract void UpdateData(ObservableCollection<DriverValueModel> models, LapTimeData liveFeed, IList<PositionChange> changes);
    }
}
