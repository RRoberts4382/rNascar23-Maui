using Microsoft.Extensions.Logging;
using rNascar23Multi.Models;

namespace rNascar23Multi.Logic
{
    public class UpdateNotificationHandler
    {
        private readonly ILogger<UpdateNotificationHandler> _logger;

        public event EventHandler<UpdateNotificationEventArgs> UpdateTimerElapsed;

        private RaceSessionDetails _sessionDetails;

        public UpdateNotificationHandler(ILogger<UpdateNotificationHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected virtual void OnUpdateTimerElapsed()
        {
            try
            {
                _logger.LogInformation("BroadcastTimerFired");

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

        public void BroadcastTimerFired()
        {
            _logger.LogInformation("BroadcastTimerFired");

            OnUpdateTimerElapsed();
        }

        public void UpdateSessionDetails(RaceSessionDetails sessionDetails)
        {
            if (sessionDetails != null)
            {
                _logger.LogInformation($"Session details updated: Series {sessionDetails.SeriesId}; Race {sessionDetails.RaceId}");
            }
            else
            {
                _logger.LogInformation("Session details update called with null details");
            }

            _sessionDetails = sessionDetails ?? throw new ArgumentNullException(nameof(sessionDetails));
        }
    }
}
