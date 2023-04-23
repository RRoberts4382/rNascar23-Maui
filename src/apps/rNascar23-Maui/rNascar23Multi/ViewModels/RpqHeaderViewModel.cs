using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Sdk.Data.LiveFeeds.Ports;
using rNascar23Multi.Sdk.LiveFeeds.Models;
using rNascar23Multi.Sdk.Schedules.Models;
using rNascar23Multi.Sdk.Schedules.Ports;

namespace rNascar23Multi.ViewModels
{
    public class RpqHeaderViewModel : ObservableObject
    {
        private ILiveFeedRepository _liveFeedRepository;
        private ISchedulesRepository _schedulesRepository;
        private SeriesEvent _seriesEvent;
        private int? _raceId = null;
        
        private RpqHeaderModel _model = new RpqHeaderModel();
        public RpqHeaderModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public RpqHeaderViewModel()
        {
            _liveFeedRepository = App.serviceProvider.GetService<ILiveFeedRepository>();

            _schedulesRepository = App.serviceProvider.GetService<ISchedulesRepository>();

            var updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

            updateHandler.UpdateTimerElapsed += UpdateTimer_UpdateTimerElapsed;
        }

        public RpqHeaderViewModel(
            ILiveFeedRepository liveFeedRepository,
            ISchedulesRepository schedulesRepository,
            UpdateNotificationHandler updateHandler)
        {
            _liveFeedRepository = liveFeedRepository ?? throw new ArgumentNullException(nameof(liveFeedRepository));
            _schedulesRepository = schedulesRepository ?? throw new ArgumentNullException(nameof(schedulesRepository));

            updateHandler.UpdateTimerElapsed += UpdateTimer_UpdateTimerElapsed;
        }

        private async void UpdateTimer_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
        {
            await LoadModelsAsync(e.SessionDetails);
        }

        private async Task LoadModelsAsync(RaceSessionDetails sessionDetails)
        {
            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            if (Model == null)
                Model = new RpqHeaderModel();

            if (_seriesEvent == null || _raceId.GetValueOrDefault(-1) != liveFeed.RaceId)
            {
                _seriesEvent = await GetCurrentSeriesEventAsync((int)liveFeed.SeriesId, liveFeed.RaceId);

                Model.FlagState = (int)liveFeed.FlagState;
                Model.EventName = BuildEventName(liveFeed, _seriesEvent);

                _raceId = liveFeed.RaceId;
            }

            Model.RaceLapInfo = (liveFeed.RunType == Sdk.Common.RunTypes.Race) ?
                $"Race: Lap {liveFeed.LapNumber} of {liveFeed.LapsInRace}" :
                "";

            Model.StageLapInfo = (liveFeed.RunType == Sdk.Common.RunTypes.Race && _seriesEvent.Stage1Laps > 0) ?
               BuildStageLaps(liveFeed.Stage.Number, liveFeed.LapNumber, liveFeed.Stage.FinishAtLap, liveFeed.Stage.LapsInStage) :
               "";
        }

        private string BuildStageLaps(int stageNumber, int lapNumber, int stageFinishAtLap, int lapsInStage)
        {
            var stageStartLap = stageFinishAtLap - lapsInStage;

            return $"Stage {stageNumber}: Lap {lapNumber - stageStartLap} of {lapsInStage}";
        }

        private string BuildEventName(LiveFeed liveFeed, SeriesEvent seriesEvent)
        {
            var eventDetails = $"{liveFeed.RunName} - {GetSeriesName((int)liveFeed.SeriesId)} - {liveFeed.TrackName} ";

            string stageDetails = String.Empty;

            if (seriesEvent != null)
            {
                stageDetails = $"({seriesEvent.Stage1Laps}/{seriesEvent.Stage2Laps}/{seriesEvent.Stage3Laps})";
            }

            return $"{eventDetails} {stageDetails}".TrimEnd();
        }

        private async Task<SeriesEvent> GetCurrentSeriesEventAsync(int seriesId, int raceId)
        {
            var schedules = await _schedulesRepository.GetRaceListAsync();

            SeriesEvent seriesEvent = null;

            switch (seriesId)
            {
                case 1:
                    seriesEvent = schedules.CupSeries.FirstOrDefault(s => s.RaceId == raceId);
                    break;
                case 2:
                    seriesEvent = schedules.XfinitySeries.FirstOrDefault(s => s.RaceId == raceId);
                    break;
                case 3:
                    seriesEvent = schedules.TruckSeries.FirstOrDefault(s => s.RaceId == raceId);
                    break;
                default:
                    break;
            }

            return seriesEvent;
        }

        private string GetSeriesName(int seriesId)
        {
            return seriesId == 1 ? "Cup Series" :
                seriesId == 2 ? "Xfinity Series" :
                seriesId == 3 ? "Craftsman Truck Series" :
                seriesId == 4 ? "ARCA Menards Series" :
                seriesId == 999 ? "Whelen Modified Tour" :
                "Unknown";
        }
    }
}