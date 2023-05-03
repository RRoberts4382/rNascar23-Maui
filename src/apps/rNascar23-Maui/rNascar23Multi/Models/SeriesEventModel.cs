using rNascar23.Sdk.Common;

namespace rNascar23Multi.Models
{
    public class SeriesEventModel : NotifyModel
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }

        private SeriesTypes _seriesId;
        public SeriesTypes SeriesId
        {
            get { return _seriesId; }
            set { _seriesId = value; OnPropertyChanged(nameof(SeriesId)); }
        }

        private int _raceId;
        public int RaceId
        {
            get { return _raceId; }
            set { _raceId = value; OnPropertyChanged(nameof(RaceId)); }
        }

        private string _eventName;
        public string EventName
        {
            get { return _eventName; }
            set { _eventName = value; OnPropertyChanged(nameof(EventName)); }
        }

        private string _track;
        public string Track
        {
            get { return _track; }
            set { _track = value; OnPropertyChanged(nameof(Track)); }
        }

        private string _distance;
        public string Distance
        {
            get { return _distance; }
            set { _distance = value; OnPropertyChanged(nameof(Distance)); }
        }

        private TvTypes _tv;
        public TvTypes Tv
        {
            get { return _tv; }
            set { _tv = value; OnPropertyChanged(nameof(Tv)); }
        }

        private RadioTypes _radio;
        public RadioTypes Radio
        {
            get { return _radio; }
            set { _radio = value; OnPropertyChanged(nameof(Radio)); }
        }

        private SatelliteTypes _satelliteRadio;
        public SatelliteTypes SatelliteRadio
        {
            get { return _satelliteRadio; }
            set { _satelliteRadio = value; OnPropertyChanged(nameof(SatelliteRadio)); }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        private string _time;
        public string Time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged(nameof(Time)); }
        }

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; OnPropertyChanged(nameof(Timestamp)); }
        }
    }
}
