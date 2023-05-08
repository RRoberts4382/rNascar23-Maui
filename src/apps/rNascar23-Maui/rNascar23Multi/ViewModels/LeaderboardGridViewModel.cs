using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.Settings.Ports;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public class LeaderboardGridViewModel : ObservableObject, INotifyUpdateTarget, INotifySettingsChanged, IDisposable
    {
        #region fields

        private ILogger<LeaderboardGridViewModel> _logger;
        private ILiveFeedRepository _liveFeedRepository;
        private ISettingsRepository _settingsRepository;

        #endregion

        #region properties

        private ObservableCollection<LeaderboardGridModel> _leftModels = new ObservableCollection<LeaderboardGridModel>();
        public ObservableCollection<LeaderboardGridModel> LeftModels
        {
            get => _leftModels;
            set => SetProperty(ref _leftModels, value);
        }

        private ObservableCollection<LeaderboardGridModel> _rightModels = new ObservableCollection<LeaderboardGridModel>();
        public ObservableCollection<LeaderboardGridModel> RightModels
        {
            get => _rightModels;
            set => SetProperty(ref _rightModels, value);
        }

        #endregion

        #region ctor

        public LeaderboardGridViewModel(
            ILogger<LeaderboardGridViewModel> logger,
            ILiveFeedRepository liveFeedRepository,
            ISettingsRepository settingsRepository)
        {
            _liveFeedRepository = liveFeedRepository ?? throw new ArgumentNullException(nameof(liveFeedRepository));

            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region public

        public void UserSettingsUpdated(SettingsModel settings)
        {
            try
            {
                ModelUpdater.ReloadModels(LeftModels);
                ModelUpdater.ReloadModels(RightModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LeaderboardViewModel:UserSettingsUpdated");
            }
        }

        public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
        {
            try
            {
                if (e.SessionDetails != null)
                {
                    await LoadModelsAsync(e.SessionDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LeaderboardViewModel:UpdateTimerElapsedAsync");
            }
        }

        #endregion

        #region private

        private async Task LoadModelsAsync(RaceSessionDetails sessionDetails)
        {
            try
            {
                var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

                UpdateModels(LeftModels, liveFeed.Vehicles.OrderBy(v => v.RunningPosition).Take(20));

                UpdateModels(RightModels, liveFeed.Vehicles.OrderBy(v => v.RunningPosition).Skip(20));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LeaderboardPage:LoadModelsAsync");
            }
        }

        private void UpdateModels(ObservableCollection<LeaderboardGridModel> models, IEnumerable<Vehicle> vehicles)
        {
            var orderedVehicles = vehicles.OrderBy(v => v.RunningPosition).ToArray();

            for (int i = 0; i < orderedVehicles.Count(); i++)
            {
                LeaderboardGridModel model;

                if (models.Count > i)
                    model = models[i];
                else
                {
                    model = new LeaderboardGridModel();

                    models.Add(model);
                }

                model.Position = orderedVehicles[i].RunningPosition;
                model.CarNumber = orderedVehicles[i].VehicleNumber;
                model.DriverName = orderedVehicles[i].Driver?.FullName;
                model.Manufacturer = orderedVehicles[i].VehicleManufacturer;
                model.Laps = orderedVehicles[i].LapsCompleted;
                model.ToLeader = orderedVehicles[i].Delta;
                // TODO Calculate ToNext
                model.ToNext = orderedVehicles[i].Delta;
                // TODO Speed versus time
                model.LastLap = orderedVehicles[i].LastLapSpeed;
                // TODO Speed versus time
                model.BestLap = orderedVehicles[i].BestLapSpeed;
                model.OnLap = orderedVehicles[i].BestLap;
                model.Status = orderedVehicles[i].Status;
            }
        }

        #endregion

        #region IDisposable

        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _logger = null;
                _liveFeedRepository = null;
                _settingsRepository = null;
            }

            _disposed = true;
        }

        #endregion
    }
}