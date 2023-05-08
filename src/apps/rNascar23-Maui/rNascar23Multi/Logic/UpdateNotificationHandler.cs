using Microsoft.Extensions.Logging;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Models;

namespace rNascar23Multi.Logic
{
    public class UpdateNotificationHandler
    {
        #region fields

        private readonly ILogger<UpdateNotificationHandler> _logger;
        private RaceSessionDetails _sessionDetails;
        private readonly ILiveFeedRepository _liveFeedRepository;

        #endregion

        #region events

        public event EventHandler<UpdateNotificationEventArgs> UpdateTimerElapsed;
        protected virtual void OnUpdateTimerElapsed()
        {
            try
            {
                var e = new UpdateNotificationEventArgs()
                {
                    SessionDetails = _sessionDetails
                };

                UpdateTimerElapsed?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnUpdateTimerElapsed");
            }
        }

        public event EventHandler<SettingsModel> UserSettingsUpdated;
        protected virtual void OnUserSettingsUpdated(SettingsModel settings)
        {
            try
            {
                UserSettingsUpdated?.Invoke(this, settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnUserSettingsUpdated");
            }
        }

        #endregion

        #region ctor

        public UpdateNotificationHandler(
            ILogger<UpdateNotificationHandler> logger,
            ILiveFeedRepository liveFeedRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _liveFeedRepository = liveFeedRepository ?? throw new ArgumentNullException(nameof(liveFeedRepository));

            BroadcastTimerFired();
        }

        #endregion

        #region public

        public async void BroadcastTimerFired()
        {
            if (_sessionDetails == null)
            {
                _sessionDetails = await GetRaceSessionDetailsAsync();
            }

            OnUpdateTimerElapsed();
        }

        public void UpdateSessionDetails(RaceSessionDetails sessionDetails)
        {
            _sessionDetails = sessionDetails ?? throw new ArgumentNullException(nameof(sessionDetails));
        }

        public void UpdateUserSettings(SettingsModel settings)
        {
            OnUserSettingsUpdated(settings);
        }

        #endregion

        #region private

        private async Task<RaceSessionDetails> GetRaceSessionDetailsAsync()
        {
            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            if (liveFeed != null)
            {
                return new RaceSessionDetails()
                {
                    RaceId = liveFeed.RaceId,
                    SeriesId = (int)liveFeed.SeriesId,
                    Year = DateTime.Now.Year
                };
            }

            return null;
        }

        #endregion
    }
}
