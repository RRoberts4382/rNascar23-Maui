namespace rNascar23.Models
{
    public class FlagsModel : NotifyModel
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

        private string _cautionFor;
        public string CautionFor
        {
            get
            {
                return _cautionFor;
            }
            set
            {
                _cautionFor = value;
                OnPropertyChanged(nameof(CautionFor));
            }
        }

        private string _luckyDog;
        public string LuckyDog
        {
            get
            {
                return _luckyDog;
            }
            set
            {
                _luckyDog = value;
                OnPropertyChanged(nameof(LuckyDog));
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
