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

        private GridViewTypes _gridViewType;
        private ILogger<DriverValueViewModel> _logger;
        private IListBuilder<DriverValueModel> _listBuilder;
        private ISettingsRepository _settingsRepository;

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

            var listBuilderFactory = App.serviceProvider.GetService<ListBuilderFactory>();

            _listBuilder = listBuilderFactory.GetListBuilder(_gridViewType);

            _settingsRepository = App.serviceProvider.GetService<ISettingsRepository>();

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
                case GridViewTypes.DriverRatings:
                    {
                        ListHeader = "Driver Ratings";
                        HeaderBackgroundColor = Colors.Gainsboro;
                        HeaderTextColor = Colors.SteelBlue;
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
                ModelUpdater.ReloadModels(Models);
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

        private async Task LoadModelsAsync(RaceSessionDetails sessionDetails)
        {
            try
            {
                switch (_gridViewType)
                {
                    case GridViewTypes.FastestLaps:
                    case GridViewTypes.LapLeaders:
                        {
                            await _listBuilder.UpdateDataAsync(Models);
                            break;
                        }
                    case GridViewTypes.Movers:
                    case GridViewTypes.Fallers:
                    case GridViewTypes.Points:
                    case GridViewTypes.StagePoints:
                    case GridViewTypes.DriverRatings:
                        {
                            if (sessionDetails != null)
                            {
                                await _listBuilder.UpdateDataAsync(
                                    Models,
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
                        {
                            if (sessionDetails != null)
                            {
                                await _listBuilder.UpdateDataAsync(
                                    Models,
                                    sessionDetails.SeriesId,
                                    sessionDetails.RaceId);
                            }

                            break;
                        }
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
                _listBuilder = null;
                _settingsRepository = null;
            }

            _disposed = true;
        }

        #endregion
    }
}
