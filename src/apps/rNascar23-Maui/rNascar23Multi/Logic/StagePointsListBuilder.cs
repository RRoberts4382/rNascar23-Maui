using rNascar23.Sdk.Common;
using rNascar23.Sdk.Points.Models;
using rNascar23.Sdk.Points.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal class StagePointsListBuilder : ListBuilder<DriverValueModel>, IListBuilder<DriverValueModel>
    {
        private IPointsRepository _pointsRepository;

        public StagePointsListBuilder(IPointsRepository pointsRepository)
        {
            _pointsRepository = pointsRepository ?? throw new ArgumentNullException(nameof(pointsRepository));
        }

        public override async Task UpdateDataAsync(ObservableCollection<DriverValueModel> models, int? seriesId, int? raceId)
        {
            if (!seriesId.HasValue)
                throw new ArgumentNullException(nameof(seriesId));

            if (!raceId.HasValue)
                throw new ArgumentNullException(nameof(raceId));

            var stagePoints = await _pointsRepository.GetStagePointsAsync((SeriesTypes)seriesId.Value, raceId.Value);

            if (stagePoints == null || !stagePoints.Any())
                return;

            UpdateData(models, stagePoints);
        }

        internal void UpdateData(ObservableCollection<DriverValueModel> models, IEnumerable<StagePointsDetails> stagePoints)
        {
            var driverValues = stagePoints.
                Where(s => (s.TotalStagePoints) > 0).
                OrderByDescending(s => (s.TotalStagePoints)).
                Select((s, i) => new DriverValueModel()
                {
                    Position = i,
                    Driver = $"{s.FirstName} {s.LastName}",
                    Value = s.TotalStagePoints
                }).
                ToList();

            ModelUpdater.UpdateModels(models, driverValues);
        }
    }
}
