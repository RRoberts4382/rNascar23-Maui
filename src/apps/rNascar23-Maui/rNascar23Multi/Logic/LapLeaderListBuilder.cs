using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal class LapLeaderListBuilder : ListBuilder<DriverValueModel>, IListBuilder<DriverValueModel>
    {
        private ILiveFeedRepository _liveFeedRepository;

        public LapLeaderListBuilder(ILiveFeedRepository liveFeedRepository)
        {
            _liveFeedRepository = liveFeedRepository ?? throw new ArgumentNullException(nameof(liveFeedRepository));
        }

        public override async Task UpdateDataAsync(ObservableCollection<DriverValueModel> models, int? seriesId = null, int? raceId = null)
        {
            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            UpdateData(models, liveFeed);
        }

        internal void UpdateData(ObservableCollection<DriverValueModel> models, LiveFeed liveFeed)
        {
            var driverValues = liveFeed.Vehicles.
               Where(v => v.LapsLed.Count > 0).
               OrderByDescending(v => v.LapsLed.Sum(l => l.EndLap - l.StartLap)).
               Take(5).
               Select((v, i) => new DriverValueModel()
               {
                   Position = i,
                   Driver = v.Driver.FullName,
                   Value = v.LapsLed.Sum(l => l.EndLap - l.StartLap)
               }).
               ToList();

            ModelUpdater.UpdateModels(models, driverValues);
        }
    }
}
