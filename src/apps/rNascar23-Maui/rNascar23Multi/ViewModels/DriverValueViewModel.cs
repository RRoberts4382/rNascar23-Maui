using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Sdk.Common;
using rNascar23Multi.Sdk.Data.LiveFeeds.Ports;
using rNascar23Multi.Sdk.LapTimes.Models;
using rNascar23Multi.Sdk.LapTimes.Ports;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public partial class DriverValueViewModel : ObservableObject
    {
        ILogger<DriverValueViewModel> _logger;
        private ILiveFeedRepository _liveFeedRepository;
        private IMoversFallersService _moversFallersService;
        private ILapTimesRepository _lapTimeRepository;
        private GridViewTypes _gridViewType;

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

        public DriverValueViewModel(GridViewTypes gridViewType)
        {
            _gridViewType = gridViewType;

            _logger = App.serviceProvider.GetService<ILogger<DriverValueViewModel>>();

            _liveFeedRepository = App.serviceProvider.GetService<ILiveFeedRepository>();

            _moversFallersService = App.serviceProvider.GetService<IMoversFallersService>();

            _lapTimeRepository = App.serviceProvider.GetService<ILapTimesRepository>();

            var updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

            updateHandler.UpdateTimerElapsed += UpdateTimer_UpdateTimerElapsed;

            switch (_gridViewType)
            {
                case GridViewTypes.FastestLaps:
                    {
                        ListHeader = "Fastest Laps";
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
                case GridViewTypes.StagePoints:
                    {
                        ListHeader = "Stage Points";
                        HeaderBackgroundColor = Colors.Blue;
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

        private async void UpdateTimer_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
        {
            try
            {
                _logger.LogInformation($"DriverValueViewModel - UpdateTimer_UpdateTimerElapsed GridViewType:{_gridViewType}");

                await LoadModelsAsync(e.SessionDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DriverValueViewModel:UpdateTimer_UpdateTimerElapsed");
            }
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
                    case GridViewTypes.StagePoints:
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
            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            Models.Clear();

            int i = 1;
            foreach (var lapLedLeader in liveFeed.Vehicles.
                Where(v => v.LapsLed.Count > 0).
                OrderByDescending(v => v.LapsLed.Sum(l => l.EndLap - l.StartLap)).
                Take(5))
            {
                var lapLeader = new DriverValueModel()
                {
                    Driver = lapLedLeader.Driver.FullName,
                    Value = lapLedLeader.LapsLed.Sum(l => l.EndLap - l.StartLap)
                };

                Models.Add(lapLeader);
                i++;
            }
        }

        private async Task BuildFastestLapsDataAsync()
        {
            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            Models.Clear();

            //if (UserSettings.FastestLapsDisplayType == SpeedTimeType.MPH)
            //{
            var laps = liveFeed.Vehicles.
                Where(v => v.BestLapSpeed > 0).
                OrderByDescending(v => v.BestLapSpeed).
                Take(5).
                Select(v => new DriverValueModel()
                {
                    Driver = v.Driver.FullName,
                    Value = (float)Math.Round(v.BestLapSpeed, 3)
                }).
                ToList();

            foreach (var lap in laps)
            {
                Models.Add(lap);
            }

            //}
            //else
            //{
            //    models = liveFeed.Vehicles.
            //        Where(v => v.BestLapTime > 0).
            //        OrderBy(v => v.BestLapTime).
            //        Take(10).
            //        Select(v => new DriverValueModel()
            //        {
            //            Driver = v.Driver.FullName,
            //            Value = (float)Math.Round(v.BestLapTime, 3)
            //        }).ToList();
            //}
        }

        private async Task BuildMoversDataAsync(int seriesId, int raceId)
        {
            LapTimeData lapTimeData = await _lapTimeRepository.GetLapTimeDataAsync((SeriesTypes)seriesId, raceId);

            Models.Clear();

            var changes = _moversFallersService.GetDriverPositionChanges(lapTimeData);

            var laps = changes.
                OrderByDescending(c => c.ChangeSinceFlagChange).
                Where(c => c.ChangeSinceFlagChange > 0).
                Select(c => new DriverValueModel()
                {
                    Driver = c.Driver,
                    Value = c.ChangeSinceFlagChange
                }).
                Take(5).
                ToList();

            foreach (var lap in laps)
            {
                Models.Add(lap);
            }
        }

        private async Task BuildFallersDataAsync(int seriesId, int raceId)
        {
            LapTimeData lapTimeData = await _lapTimeRepository.GetLapTimeDataAsync((SeriesTypes)seriesId, raceId);

            Models.Clear();

            var changes = _moversFallersService.GetDriverPositionChanges(lapTimeData);

            var laps = changes.
                OrderBy(c => c.ChangeSinceFlagChange).
                Where(c => c.ChangeSinceFlagChange < 0).
                Select(c => new DriverValueModel()
                {
                    Driver = c.Driver,
                    Value = c.ChangeSinceFlagChange
                }).
                ToList();

            foreach (var lap in laps)
            {
                Models.Add(lap);
            }
        }
    }
}
