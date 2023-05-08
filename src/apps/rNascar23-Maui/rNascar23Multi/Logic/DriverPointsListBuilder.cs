using rNascar23.Sdk.Common;
using rNascar23.Sdk.Points.Models;
using rNascar23.Sdk.Points.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal class DriverPointsListBuilder : ListBuilder<DriverValueModel>, IListBuilder<DriverValueModel>
    {
        private IPointsRepository _pointsRepository;

        public DriverPointsListBuilder(IPointsRepository pointsRepository)
        {
            _pointsRepository = pointsRepository ?? throw new ArgumentNullException(nameof(pointsRepository));
        }

        public override async Task UpdateDataAsync(ObservableCollection<DriverValueModel> models, int? seriesId, int? raceId)
        {
            if (!seriesId.HasValue)
                throw new ArgumentNullException(nameof(seriesId));

            if (!raceId.HasValue)
                throw new ArgumentNullException(nameof(raceId));

            var driverPoints = await _pointsRepository.GetDriverPointsAsync((SeriesTypes)seriesId.Value, raceId.Value);

            UpdateData(models, driverPoints);
        }

        internal void UpdateData(ObservableCollection<DriverValueModel> models, IEnumerable<DriverPoints> driverPoints)
        {
            var driverValues = driverPoints.
                Select(p => new DriverValueModel()
                {
                    Position = p.PointsPosition,
                    Driver = p.Driver,
                    Value = p.Points
                }).OrderBy(p => p.Position).ToList();

            ModelUpdater.UpdateModels(models, driverValues);
        }
    }
}
