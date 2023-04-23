namespace rNascar23Multi.Models
{
    public class KeyMomentsModel : NotifyModel
    {
        private int _flagState;
        public int FlagState
        {
            get
            {
                return _flagState;
            }
            set
            {
                _flagState = value;
                OnPropertyChanged(nameof(FlagState));
            }
        }

        private int _lap;
        public int Lap
        {
            get
            {
                return _lap;
            }
            set
            {
                _lap = value;
                OnPropertyChanged(nameof(Lap));
            }
        }

        private string _comments;
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
                OnPropertyChanged(nameof(Timestamp));
            }
        }
    }
}
