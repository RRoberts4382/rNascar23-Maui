using rNascar23Multi.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using rNascar23Multi.Logic;
using rNascar23Multi.Sdk.Points.Ports;
using rNascar23Multi.Sdk.Common;

namespace rNascar23Multi.ViewModels
{
    public partial class PositionDriverValueViewModel : ObservableObject
    {
        ILogger<DriverValueViewModel> _logger;
        IPointsRepository _pointsRepository;

        private GridViewTypes _gridViewType;

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

        public PositionDriverValueViewModel(GridViewTypes gridViewType)
        {
            _gridViewType = gridViewType;

            _logger = App.serviceProvider.GetService<ILogger<DriverValueViewModel>>();

            _pointsRepository = App.serviceProvider.GetService<IPointsRepository>();

            var updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

            updateHandler.UpdateTimerElapsed += UpdateTimer_UpdateTimerElapsed;

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

        private async void UpdateTimer_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
        {
            try
            {
                _logger.LogInformation($"PositionDriverValueViewModel - UpdateTimer_UpdateTimerElapsed GridViewType:{_gridViewType}");

                await LoadModelsAsync(e.SessionDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PositionDriverValueViewModel:UpdateTimer_UpdateTimerElapsed");
            }
        }

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
            var driverPoints = await _pointsRepository.GetDriverPointsAsync((SeriesTypes)seriesId, raceId);

            Models.Clear();

            var models = driverPoints.
                Select(p => new PositionDriverValueModel()
                {
                    Position = p.PointsPosition,
                    Driver = p.Driver,
                    Value = p.Points
                }).OrderBy(p => p.Position).ToList();

            foreach (var model in models)
            {
                Models.Add(model);
            }
        }

        private async Task BuildStagePointsDataAsync(int seriesId, int raceId)
        {
            var stagePoints = await _pointsRepository.GetStagePointsAsync((SeriesTypes)seriesId, raceId);

            Models.Clear();

            if (stagePoints == null || stagePoints.Count() == 0)
                return;

            //stagePoints = stagePoints.Where(s => s.RaceId == raceId).ToList();

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

                Models.Add(model);

                i++;
            }
        }
    }
}
