using Microsoft.Extensions.Logging;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Models;

namespace rNascar23Multi.Logic
{
    public class UpdateNotificationHandler
    {
        #region fields

        private readonly ILogger<UpdateNotificationHandler> _logger;
        private RaceSessionDetails _sessionDetails;

        #endregion

        #region events

        public event EventHandler<UpdateNotificationEventArgs> UpdateTimerElapsed;
        protected virtual void OnUpdateTimerElapsed()
        {
            try
            {
                //_logger.LogInformation("UpdateNotificationHandler - OnUpdateTimerElapsed");

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
                //_logger.LogInformation("OnUserSettingsUpdated");

                UserSettingsUpdated?.Invoke(this, settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnUserSettingsUpdated");
            }
        }

        #endregion

        #region ctor

        public UpdateNotificationHandler(ILogger<UpdateNotificationHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            //_logger.LogInformation("UpdateNotificationHandler - ctor");
        }

        #endregion

        #region public

        public void BroadcastTimerFired()
        {
            //_logger.LogInformation("UpdateNotificationHandler - BroadcastTimerFired");

            OnUpdateTimerElapsed();
        }

        public void UpdateSessionDetails(RaceSessionDetails sessionDetails)
        {
            //if (sessionDetails != null)
            //{
            //    _logger.LogInformation($"UpdateNotificationHandler - Session details updated: Series {sessionDetails.SeriesId}; Race {sessionDetails.RaceId}");
            //}
            //else
            //{
            //    _logger.LogInformation("UpdateNotificationHandler - Session details update called with null details");
            //}

            _sessionDetails = sessionDetails ?? throw new ArgumentNullException(nameof(sessionDetails));
        }

        public void UpdateUserSettings(SettingsModel settings)
        {
            //_logger.LogInformation("UpdateNotificationHandler - UpdateUserSettings");

            OnUserSettingsUpdated(settings);
        }

        #endregion
    }
}
