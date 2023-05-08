using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.Settings.Ports;
using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal class FastestLapsListBuilder : ListBuilder<DriverValueModel>, IListBuilder<DriverValueModel>
    {
        private ILiveFeedRepository _liveFeedRepository;
        private ISettingsRepository _settingsRepository;

        public FastestLapsListBuilder(ILiveFeedRepository liveFeedRepository, ISettingsRepository settingsRepository)
        {
            _liveFeedRepository = liveFeedRepository ?? throw new ArgumentNullException(nameof(liveFeedRepository));
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
        }

        public override async Task UpdateDataAsync(ObservableCollection<DriverValueModel> models, int? seriesId = null, int? raceId = null)
        {
            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            var userSettings = _settingsRepository.GetSettings();

            UpdateData(models, liveFeed, userSettings);
        }

        internal void UpdateData(ObservableCollection<DriverValueModel> models, LiveFeed liveFeed, SettingsModel userSettings)
        {
            IList<DriverValueModel> driverValues;

            if (userSettings.UseMph)
            {
                driverValues = liveFeed.Vehicles.
                    Where(v => v.BestLapSpeed > 0).
                    OrderByDescending(v => v.BestLapSpeed).
                    Take(5).
                    Select((v, i) => new DriverValueModel()
                    {
                        Position = i,
                        Driver = v.Driver.FullName,
                        Value = (float)Math.Round(v.BestLapSpeed, 3)
                    }).
                    ToList();
            }
            else
            {
                driverValues = liveFeed.Vehicles.
                    Where(v => v.BestLapTime > 0).
                    OrderBy(v => v.BestLapTime).
                    Take(10).
                    Select((v, i) => new DriverValueModel()
                    {
                        Position = i,
                        Driver = v.Driver.FullName,
                        Value = (float)Math.Round(v.BestLapTime, 3)
                    }).ToList();
            }

            ModelUpdater.UpdateModels(models, driverValues);
        }
    }
}
