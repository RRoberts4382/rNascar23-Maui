using rNascar23.Sdk.Common;
using rNascar23.Sdk.LoopData.Models;
using rNascar23.Sdk.LoopData.Ports;
using rNascar23.Sdk.Points.Models;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal class DriverRatingsListBuilder : ListBuilder<DriverValueModel>, IListBuilder<DriverValueModel>
    {
        private ILoopDataRepository _loopDataRepository;

        public DriverRatingsListBuilder(ILoopDataRepository loopDataRepository)
        {
            _loopDataRepository = loopDataRepository ?? throw new ArgumentNullException(nameof(loopDataRepository));
        }

        public override async Task UpdateDataAsync(ObservableCollection<DriverValueModel> models, int? seriesId, int? raceId)
        {
            if (!seriesId.HasValue)
                throw new ArgumentNullException(nameof(seriesId));

            if (!raceId.HasValue)
                throw new ArgumentNullException(nameof(raceId));

            var driverRatings = await _loopDataRepository.GetLoopDataRatingsAsync((SeriesTypes)seriesId.Value, raceId.Value);

            UpdateData(models, driverRatings);
        }

        internal void UpdateData(ObservableCollection<DriverValueModel> models, IEnumerable<DriverRatingInfo> driverPoints)
        {
            var driverValues = driverPoints.
                Select(p => new DriverValueModel()
                {
                    Position = p.Position,
                    Driver = p.Driver,
                    Value = p.Rating
                }).OrderBy(p => p.Position).ToList();

            ModelUpdater.UpdateModels(models, driverValues);
        }
    }
}
