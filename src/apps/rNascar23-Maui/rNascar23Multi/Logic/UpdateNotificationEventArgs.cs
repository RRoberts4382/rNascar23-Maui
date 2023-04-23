using rNascar23Multi.Models;

namespace rNascar23Multi.Logic
{
    public class UpdateNotificationEventArgs : EventArgs
    {
        public RaceSessionDetails SessionDetails { get; set; }
    }
}
