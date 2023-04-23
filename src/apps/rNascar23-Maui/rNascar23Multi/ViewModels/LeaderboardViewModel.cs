using rNascar23Multi.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;
using rNascar23Multi.Sdk.Data.LiveFeeds.Ports;
using rNascar23Multi.Logic;
using System.Diagnostics;
using rNascar23Multi.Sdk.LiveFeeds.Models;
using Microsoft.Extensions.Logging;

namespace rNascar23Multi.ViewModels
{
    public class LeaderboardViewModel : ObservableObject
    {
        ILogger<LeaderboardViewModel> _logger;
        private ILiveFeedRepository _liveFeedRepository;
        private UpdateNotificationHandler _updateHandler;

        private ObservableCollection<LeaderboardModel> _leftModels = new ObservableCollection<LeaderboardModel>();
        private ObservableCollection<LeaderboardModel> _rightModels = new ObservableCollection<LeaderboardModel>();

        public ObservableCollection<LeaderboardModel> LeftModels
        {
            get => _leftModels;
            set => SetProperty(ref _leftModels, value);
        }
        public ObservableCollection<LeaderboardModel> RightModels
        {
            get => _rightModels;
            set => SetProperty(ref _rightModels, value);
        }

        public LeaderboardViewModel()
        {
            _liveFeedRepository = App.serviceProvider.GetService<ILiveFeedRepository>();

            _logger = App.serviceProvider.GetService<ILogger<LeaderboardViewModel>>();

            _updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

            _updateHandler.UpdateTimerElapsed += UpdateTimer_UpdateTimerElapsed;
        }

        public LeaderboardViewModel(
            ILogger<LeaderboardViewModel> logger,
            ILiveFeedRepository liveFeedRepository,
            UpdateNotificationHandler updateHandler)
        {
            _liveFeedRepository = liveFeedRepository ?? throw new ArgumentNullException(nameof(liveFeedRepository));

            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _updateHandler.UpdateTimerElapsed += UpdateTimer_UpdateTimerElapsed;
        }

        private async void UpdateTimer_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
        {
            try
            {
                _logger.LogInformation($"LeaderboardViewModel - UpdateTimer_UpdateTimerElapsed");

                await LoadModelsAsync(e.SessionDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LeaderboardViewModel:UpdateTimer_UpdateTimerElapsed");
            }
        }

        private async Task LoadModelsAsync(RaceSessionDetails sessionDetails)
        {
            try
            {
                var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

                if ((sessionDetails == null) || (int)liveFeed.SeriesId != sessionDetails.SeriesId ||
                    liveFeed.RaceId != sessionDetails.RaceId)
                {
                    _updateHandler.UpdateSessionDetails(new RaceSessionDetails()
                    {
                        SeriesId = (int)liveFeed.SeriesId,
                        RaceId = liveFeed.RaceId,
                        Year = DateTime.Now.Year
                    });
                }

                UpdateModels(LeftModels, liveFeed.Vehicles.OrderBy(v => v.RunningPosition).Take(20));

                UpdateModels(RightModels, liveFeed.Vehicles.OrderBy(v => v.RunningPosition).Skip(20));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LeaderboardViewModel:LoadModelsAsync");
            }
        }

        private void UpdateModels(ObservableCollection<LeaderboardModel> models, IEnumerable<Vehicle> vehicles)
        {
            var orderedVehicles = vehicles.OrderBy(v => v.RunningPosition).ToArray();

            for (int i = 0; i < orderedVehicles.Count(); i++)
            {
                LeaderboardModel model;

                if (models.Count > i)
                    model = models[i];
                else
                {
                    model = new LeaderboardModel();

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
                model.Status = (int)orderedVehicles[i].Status;
            }
        }

        private void LoadFromSource()
        {
            for (int i = 0; i < 20; i++)
            {
                LeftModels.Add(new LeaderboardModel()
                {
                    Position = i + 1,
                    CarNumber = i.ToString(),
                    DriverName = $"Driver #{i}"
                });
            }
            for (int i = 20; i < 40; i++)
            {
                RightModels.Add(new LeaderboardModel()
                {
                    Position = i + 1,
                    CarNumber = i.ToString(),
                    DriverName = $"Driver #{i}"
                });
            }
        }
    }
}