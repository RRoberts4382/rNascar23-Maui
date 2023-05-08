using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23.Sdk.Points.Models;
using rNascar23.Sdk.Points.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.Settings.Ports;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace rNascar23Multi.ViewModels
{
    public partial class DriverValueViewModel : ObservableObject, INotifyUpdateTarget, INotifySettingsChanged, IDisposable
    {
        #region fields

        ILogger<DriverValueViewModel> _logger;
        private ILiveFeedRepository _liveFeedRepository;
        private IMoversFallersService _moversFallersService;
        private ILapTimesRepository _lapTimeRepository;
        private ISettingsRepository _settingsRepository;
        private IPointsRepository _pointsRepository;
        private GridViewTypes _gridViewType;

        #endregion

        #region properties

        private ObservableCollection<DriverValueModel> _models = new ObservableCollection<DriverValueModel>();
        public ObservableCollection<DriverValueModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _listHeader = "Driver/Value Grid";
        public string ListHeader
        {
            get => _listHeader;
            set => SetProperty(ref _listHeader, value);
        }

        private Color _headerTextColor = Colors.Black;
        public Color HeaderTextColor
        {
            get => _headerTextColor;
            set => SetProperty(ref _headerTextColor, value);
        }

        private Color _headerBackgroundColor = Colors.White;
        public Color HeaderBackgroundColor
        {
            get => _headerBackgroundColor;
            set => SetProperty(ref _headerBackgroundColor, value);
        }

        #endregion

        #region ctor

        public DriverValueViewModel(GridViewTypes gridViewType)
        {
            _gridViewType = gridViewType;

            _logger = App.serviceProvider.GetService<ILogger<DriverValueViewModel>>();

            _liveFeedRepository = App.serviceProvider.GetService<ILiveFeedRepository>();

            _moversFallersService = App.serviceProvider.GetService<IMoversFallersService>();

            _lapTimeRepository = App.serviceProvider.GetService<ILapTimesRepository>();

            _settingsRepository = App.serviceProvider.GetService<ISettingsRepository>();

            _pointsRepository = App.serviceProvider.GetService<IPointsRepository>();

            var userSettings = _settingsRepository.GetSettings();

            switch (_gridViewType)
            {
                case GridViewTypes.FastestLaps:
                    {
                        ListHeader = userSettings.UseMph ? "Fastest Laps (M.P.H.)" : "Fastest Laps (Lap Time)";
                        HeaderTextColor = Colors.Black;
                        HeaderBackgroundColor = Colors.LightSteelBlue;
                        break;
                    }
                case GridViewTypes.LapLeaders:
                    {
                        ListHeader = "Lap Leaders";
                        HeaderTextColor = Colors.White;
                        HeaderBackgroundColor = Colors.DarkBlue;
                        break;
                    }
                case GridViewTypes.Movers:
                    {
                        ListHeader = "Movers";
                        HeaderBackgroundColor = Colors.Green;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.Fallers:
                    {
                        ListHeader = "Fallers";
                        HeaderBackgroundColor = Colors.Red;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.Best5Laps:
                case GridViewTypes.Best10Laps:
                case GridViewTypes.Best15Laps:
                    {
                        ListHeader = gridViewType.ToString();
                        HeaderBackgroundColor = Colors.LightSkyBlue;
                        HeaderTextColor = Colors.Black;
                        break;
                    }
                case GridViewTypes.Last5Laps:
                case GridViewTypes.Last10Laps:
                case GridViewTypes.Last15Laps:
                    {
                        ListHeader = gridViewType.ToString();
                        HeaderBackgroundColor = Colors.Blue;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.Points:
                    {
                        ListHeader = "Driver Points";
                        HeaderBackgroundColor = Colors.Orange;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.StagePoints:
                    {
                        ListHeader = "Stage Points";
                        HeaderBackgroundColor = Colors.Teal;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.None:
                default:
                    {
                        ListHeader = gridViewType.ToString();
                        HeaderBackgroundColor = Colors.Black;
                        HeaderTextColor = Colors.White;
                        break;
                    }
            }
        }

        #endregion

        #region public

        public void UserSettingsUpdated(SettingsModel settings)
        {
            try
            {
                ReloadModels();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DriverValueViewModel:UserSettingsUpdated");
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
                _logger.LogError(ex, "Error in DriverValueViewModel:UpdateTimerElapsedAsync");
            }
        }

        #endregion

        #region private

        private void ReloadModels()
        {
            var existing = Models.ToList();

            Models.Clear();

            UpdateModels(existing);
        }

        private async Task LoadModelsAsync(RaceSessionDetails sessionDetails)
        {
            try
            {
                switch (_gridViewType)
                {
                    case GridViewTypes.FastestLaps:
                        {
                            await BuildFastestLapsDataAsync();
                            break;
                        }
                    case GridViewTypes.LapLeaders:
                        {
                            await BuildLapLeadersDataAsync();
                            break;
                        }
                    case GridViewTypes.Movers:
                        {
                            if (sessionDetails != null)
                            {
                                await BuildMoversDataAsync(
                                    sessionDetails.SeriesId,
                                    sessionDetails.RaceId);
                            }

                            break;
                        }
                    case GridViewTypes.Fallers:
                        {
                            if (sessionDetails != null)
                            {
                                await BuildFallersDataAsync(
                                    sessionDetails.SeriesId,
                                    sessionDetails.RaceId);
                            }

                            break;
                        }
                    case GridViewTypes.Points:
                        {
                            if (sessionDetails != null)
                            {
                                await BuildDriverPointsDataAsync(
                                    sessionDetails.SeriesId,
                                    sessionDetails.RaceId);
                            }

                            break;
                        }
                    case GridViewTypes.StagePoints:
                        {
                            if (sessionDetails != null)
                            {
                                await BuildStagePointsDataAsync(
                                    sessionDetails.SeriesId,
                                    sessionDetails.RaceId);
                            }

                            break;
                        }
                    case GridViewTypes.Best5Laps:
                    case GridViewTypes.Best10Laps:
                    case GridViewTypes.Best15Laps:
                    case GridViewTypes.Last5Laps:
                    case GridViewTypes.Last10Laps:
                    case GridViewTypes.Last15Laps:
                    case GridViewTypes.None:
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DriverValueViewModel:LoadModelsAsync GridViewType:{_gridViewType}");
            }
        }

        private async Task BuildLapLeadersDataAsync()
        {
            IList<DriverValueModel> driverValues = new List<DriverValueModel>();

            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            driverValues = liveFeed.Vehicles.
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

            UpdateModels(driverValues);
        }

        private async Task BuildFastestLapsDataAsync()
        {
            IList<DriverValueModel> driverValues;

            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            var userSettings = _settingsRepository.GetSettings();

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

            UpdateModels(driverValues);
        }

        private async Task BuildMoversDataAsync(int seriesId, int raceId)
        {
            LapTimeData lapTimeData = await _lapTimeRepository.GetLapTimeDataAsync((SeriesTypes)seriesId, raceId);

            var changes = _moversFallersService.GetDriverPositionChanges(lapTimeData);

            var driverValues = changes.
                OrderByDescending(c => c.ChangeSinceFlagChange).
                Where(c => c.ChangeSinceFlagChange > 0).
                Select((c, i) => new DriverValueModel()
                {
                    Position = i,
                    Driver = c.Driver,
                    Value = c.ChangeSinceFlagChange
                }).
                Take(5).
                ToList();

            UpdateModels(driverValues);
        }

        private async Task BuildFallersDataAsync(int seriesId, int raceId)
        {
            LapTimeData lapTimeData = await _lapTimeRepository.GetLapTimeDataAsync((SeriesTypes)seriesId, raceId);

            var changes = _moversFallersService.GetDriverPositionChanges(lapTimeData);

            var driverValues = changes.
                OrderBy(c => c.ChangeSinceFlagChange).
                Where(c => c.ChangeSinceFlagChange < 0).
                Select((c, i) => new DriverValueModel()
                {
                    Position = i,
                    Driver = c.Driver,
                    Value = c.ChangeSinceFlagChange
                }).
                ToList();

            UpdateModels(driverValues);
        }

        private async Task BuildDriverPointsDataAsync(int seriesId, int raceId)
        {
            var driverPoints = await _pointsRepository.GetDriverPointsAsync((SeriesTypes)seriesId, raceId);

            var driverValues = driverPoints.
                Select(p => new DriverValueModel()
                {
                    Position = p.PointsPosition,
                    Driver = p.Driver,
                    Value = p.Points
                }).OrderBy(p => p.Position).ToList();

            UpdateModels(driverValues);
        }

        private async Task BuildStagePointsDataAsync(int seriesId, int raceId)
        {
            var stagePoints = await _pointsRepository.GetStagePointsAsync((SeriesTypes)seriesId, raceId);

            if (stagePoints == null || !stagePoints.Any())
                return;

            var driverValues = stagePoints.
                Where(s => (s.Stage1Points + s.Stage2Points + s.Stage3Points) > 0).
                OrderByDescending(s => (s.Stage1Points + s.Stage2Points + s.Stage3Points)).
                Select((s, i) => new DriverValueModel()
                {
                    Position = i,
                    Driver = $"{s.FirstName} {s.LastName}",
                    Value = s.Stage1Points + s.Stage2Points + s.Stage3Points
                }).
                ToList();

            UpdateModels(driverValues);
        }

        private void UpdateModels(IList<DriverValueModel> driverValues)
        {
            if (Models.Count > driverValues.Count)
            {
                for (int i = driverValues.Count - 1; i > Models.Count - 1; i--)
                {
                    Models.RemoveAt(i);
                }
            }

            for (int i = 0; i < driverValues.Count; i++)
            {
                if (Models.Count <= i)
                {
                    Models.Add(driverValues[i]);
                }
                else
                {
                    Models[i].Driver = driverValues[i].Driver;
                    Models[i].Value = driverValues[i].Value;
                }
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
                _moversFallersService = null;
                _lapTimeRepository = null;
            }

            _disposed = true;
        }

        #endregion
    }
}
