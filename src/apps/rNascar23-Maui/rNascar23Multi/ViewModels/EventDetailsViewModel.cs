using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.LoopData.Ports;
using rNascar23.Sdk.Schedules.Ports;
using rNascar23Multi.Models;

namespace rNascar23Multi.ViewModels
{
    public partial class EventDetailsViewModel : ObservableObject, IDisposable
    {
        #region fields

        private ILogger<EventDetailsViewModel> _logger;
        private ISchedulesRepository _schedulesRepository;
        private IDriverInfoRepository _driverInfoRepository;

        #endregion

        #region properties

        private EventDetailsModel _model = new EventDetailsModel();
        public EventDetailsModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        #endregion

        #region ctor

        public EventDetailsViewModel(
            ILogger<EventDetailsViewModel> logger,
            ISchedulesRepository schedulesRepository,
            IDriverInfoRepository driverInfoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _schedulesRepository = schedulesRepository ?? throw new ArgumentNullException(nameof(schedulesRepository));
            _driverInfoRepository = driverInfoRepository ?? throw new ArgumentNullException(nameof(driverInfoRepository));
        }

        public EventDetailsViewModel()
        {
            _logger = App.serviceProvider.GetRequiredService<ILogger<EventDetailsViewModel>>();

            _schedulesRepository = App.serviceProvider.GetRequiredService<ISchedulesRepository>();
            _driverInfoRepository = App.serviceProvider.GetRequiredService<IDriverInfoRepository>();
        }

        #endregion

        #region public

        public async Task LoadEventDetailsAsync(int raceId)
        {
            try
            {
                var seriesEvent = await _schedulesRepository.GetEventAsync(raceId);

                Model = new EventDetailsModel()
                {
                    Leaders = seriesEvent.NumberOfLeaders,
                    LeadChanges = seriesEvent.NumberOfLeadChanges,
                    AverageSpeed = seriesEvent.AverageSpeed,
                    CarsInField = seriesEvent.NumberOfCarsInField,
                    CautionLaps = seriesEvent.NumberOfCautionLaps,
                    Cautions = seriesEvent.NumberOfCautions,
                    MarginOfVictory = seriesEvent.MarginOfVictory,
                    PoleSpeed = seriesEvent.PoleWinnerSpeed,
                    RaceTime = seriesEvent.TotalRaceTime,
                    Summary = seriesEvent.RaceComments,
                    PoleWinner = await GetDriverNameAsync(seriesEvent.PoleWinnerDriverId),
                    Winner = await GetDriverNameAsync(seriesEvent.WinnerDriverId.GetValueOrDefault(0)),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EventDetailsViewModel:LoadEventDetailsAsync");
            }
        }

        private async Task<string> GetDriverNameAsync(int driverId)
        {
            var driver = await _driverInfoRepository.GetDriverAsync(driverId);

            return driver == null ? "None" : driver.Name;
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
                _schedulesRepository = null;
                _driverInfoRepository = null;
            }

            _disposed = true;
        }

        #endregion
    }
}
