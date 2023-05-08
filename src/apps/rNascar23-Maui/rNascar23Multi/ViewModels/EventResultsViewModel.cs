using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public partial class EventResultsViewModel : ObservableObject, IDisposable
    {
        #region fields

        private ILogger<EventResultsViewModel> _logger;
        private IWeekendFeedRepository _weekendFeedRepository;

        #endregion

        #region properties

        private ObservableCollection<EventResultModel> _models = new ObservableCollection<EventResultModel>();
        public ObservableCollection<EventResultModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        #endregion

        #region ctor

        public EventResultsViewModel(
            ILogger<EventResultsViewModel> logger,
            IWeekendFeedRepository weekendFeedRepository)
        {
            _weekendFeedRepository = weekendFeedRepository ?? throw new ArgumentNullException(nameof(weekendFeedRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public EventResultsViewModel()
        {
            _logger = App.serviceProvider.GetRequiredService<ILogger<EventResultsViewModel>>();
            _weekendFeedRepository = App.serviceProvider.GetRequiredService<IWeekendFeedRepository>();
        }

        #endregion

        #region public

        public async Task LoadEventResultsAsync(SeriesTypes seriesId, int raceId)
        {
            try
            {
                Models.Clear();

                IList<EventResultModel> eventResults = new List<EventResultModel>();

                var weekendFeed = await _weekendFeedRepository.GetWeekendFeedAsync(seriesId, raceId);

                var weekendRace = weekendFeed.WeekendRaces.FirstOrDefault();

                var weekendRaceResults = weekendRace.
                    Results.
                    Where(r => r.FinishingPosition > 0).
                    OrderBy(r => r.FinishingPosition);

                foreach (Result driverResult in weekendRaceResults)
                {
                    var eventResult = new EventResultModel()
                    {
                        FinishingPosition = driverResult.FinishingPosition,
                        CarNumber = driverResult.CarNumber,
                        Driver = driverResult.DriverFullName,
                        Vehicle = driverResult.CarModel,
                        Hometown = driverResult.DriverHometown,
                        Sponsor = driverResult.Sponsor,
                        Owner = driverResult.OwnerFullName,
                        FinishingStatus = driverResult.FinishingStatus,
                        CrewChief = driverResult.CrewChiefFullName,
                        LapsLed = driverResult.LapsLed,
                        LapsCompleted = driverResult.LapsCompleted,
                        PointsEarned = driverResult.PointsEarned,
                        PlayoffPointsEarned = driverResult.PlayoffPointsEarned,
                        StartingPosition = driverResult.StartingPosition,
                    };

                    eventResults.Add(eventResult);
                }

                Models = new ObservableCollection<EventResultModel>(eventResults.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EventResultsViewModel:LoadEventResultsAsync");
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
                _weekendFeedRepository = null;
            }

            _disposed = true;
        }

        #endregion
    }
}
