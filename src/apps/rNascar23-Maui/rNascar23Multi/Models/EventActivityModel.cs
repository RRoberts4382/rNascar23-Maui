using rNascar23.Sdk.Common;

namespace rNascar23Multi.Models
{
    public class EventActivityModel : NotifyModel
    {
        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; OnPropertyChanged(nameof(Timestamp)); }
        }

        private SeriesTypes _seriesId;
        public SeriesTypes SeriesId
        {
            get { return _seriesId; }
            set { _seriesId = value; OnPropertyChanged(nameof(SeriesId)); }
        }

        private string _activityName;
        public string ActivityName
        {
            get { return _activityName; }
            set { _activityName = value; OnPropertyChanged(nameof(ActivityName)); }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(nameof(Notes)); }
        }

        private string _onTrack;
        public string OnTrack
        {
            get { return _onTrack; }
            set { _onTrack = value; OnPropertyChanged(nameof(OnTrack)); }
        }
    }
}
