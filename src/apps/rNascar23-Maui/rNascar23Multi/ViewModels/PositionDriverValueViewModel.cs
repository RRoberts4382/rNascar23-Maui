using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.Points.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public partial class PositionDriverValueViewModel : ObservableObject, INotifyUpdateTarget, IDisposable
    {
        #region fields

        private ILogger<DriverValueViewModel> _logger;
        private IPointsRepository _pointsRepository;
        private GridViewTypes _gridViewType;

        #endregion

        #region properties

        private ObservableCollection<PositionDriverValueModel> _models = new ObservableCollection<PositionDriverValueModel>();
        public ObservableCollection<PositionDriverValueModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _listHeader = "Position/Driver/Value Grid";
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

        public PositionDriverValueViewModel(GridViewTypes gridViewType)
        {
            _gridViewType = gridViewType;

            _logger = App.serviceProvider.GetService<ILogger<DriverValueViewModel>>();

            _pointsRepository = App.serviceProvider.GetService<IPointsRepository>();

            switch (gridViewType)
            {
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

        public async Task UserSettingsUpdatedAsync()
        {

        }

        public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
        {
            try
            {
                await LoadModelsAsync(e.SessionDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PositionDriverValueViewModel:UpdateTimer_UpdateTimerElapsed");
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
                    case GridViewTypes.Movers:
                    case GridViewTypes.Fallers:
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

        private async Task BuildDriverPointsDataAsync(int seriesId, int raceId)
        {
            IList<PositionDriverValueModel> driverValues = new List<PositionDriverValueModel>();

            var driverPoints = await _pointsRepository.GetDriverPointsAsync((SeriesTypes)seriesId, raceId);

            driverValues = driverPoints.
                Select(p => new PositionDriverValueModel()
                {
                    Position = p.PointsPosition,
                    Driver = p.Driver,
                    Value = p.Points
                }).OrderBy(p => p.Position).ToList();

            UpdateModels(driverValues);
        }

        private async Task BuildStagePointsDataAsync(int seriesId, int raceId)
        {
            IList<PositionDriverValueModel> driverValues = new List<PositionDriverValueModel>();

            var stagePoints = await _pointsRepository.GetStagePointsAsync((SeriesTypes)seriesId, raceId);

            if (stagePoints == null || stagePoints.Count() == 0)
                return;

            int i = 1;
            foreach (var driverStagePoints in stagePoints.
                Where(s => (s.Stage1Points + s.Stage2Points + s.Stage3Points) > 0).
                OrderByDescending(s => (s.Stage1Points + s.Stage2Points + s.Stage3Points)))
            {
                var model = new PositionDriverValueModel()
                {
                    Position = i,
                    Driver = $"{driverStagePoints.FirstName} {driverStagePoints.LastName}",
                    Value = driverStagePoints.Stage1Points + driverStagePoints.Stage2Points + driverStagePoints.Stage3Points
                };

                driverValues.Add(model);

                i++;
            }

            UpdateModels(driverValues);
        }

        private void UpdateModels(IList<PositionDriverValueModel> driverValues)
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
                    Models[i].Position = driverValues[i].Position;
                    Models[i].Driver = driverValues[i].Driver;
                    Models[i].Value = driverValues[i].Value;
                }
            }
        }

        #endregion

        #region IDisposed

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
                _pointsRepository = null;
            }
            // free native resources if there are any.

            _disposed = true;
        }

        #endregion
    }
}
