namespace rNascar23Multi.Models
{
    public class EventResultModel : NotifyModel
    {
        private int _finishingPosition;
        private string _carNumber;
        private string _driver;
        private string _vehicle;
        private string _hometown;
        private string _sponsor;
        private string _owner;
        private string _crewChief;
        private string _finishingStatus;
        private int _startingPosition;
        private int _lapsLed;
        private int _lapsCompleted;
        private int _pointsEarned;
        private int _playoffPointsEarned;

        public int FinishingPosition
        {
            get { return _finishingPosition; }
            set
            {
                if (_finishingPosition != value)
                {
                    _finishingPosition = value;
                    OnPropertyChanged("FinishingPosition");
                }
            }
        }

        public string CarNumber
        {
            get { return _carNumber; }
            set
            {
                if (_carNumber != value)
                {
                    _carNumber = value;
                    OnPropertyChanged("CarNumber");
                }
            }
        }

        public string Driver
        {
            get { return _driver; }
            set
            {
                if (_driver != value)
                {
                    _driver = value;
                    OnPropertyChanged("Driver");
                }
            }
        }

        public string Vehicle
        {
            get { return _vehicle; }
            set
            {
                if (_vehicle != value)
                {
                    _vehicle = value;
                    OnPropertyChanged("Vehicle");
                }
            }
        }

        public string Hometown
        {
            get { return _hometown; }
            set
            {
                if (_hometown != value)
                {
                    _hometown = value;
                    OnPropertyChanged("Hometown");
                }
            }
        }

        public string Sponsor
        {
            get { return _sponsor; }
            set
            {
                if (_sponsor != value)
                {
                    _sponsor = value;
                    OnPropertyChanged("Sponsor");
                }
            }
        }

        public string Owner
        {
            get { return _owner; }
            set
            {
                if (_owner != value)
                {
                    _owner = value;
                    OnPropertyChanged("Owner");
                }
            }
        }

        public string CrewChief
        {
            get { return _crewChief; }
            set
            {
                if (_crewChief != value)
                {
                    _crewChief = value;
                    OnPropertyChanged("CrewChief");
                }
            }
        }

        public string FinishingStatus
        {
            get { return _finishingStatus; }
            set
            {
                if (_finishingStatus != value)
                {
                    _finishingStatus = value;
                    OnPropertyChanged("FinishingStatus");
                }
            }
        }

        public int StartingPosition
        {
            get { return _startingPosition; }
            set
            {
                if (_startingPosition != value)
                {
                    _startingPosition = value;
                    OnPropertyChanged("StartingPosition");
                }
            }
        }

        public int LapsLed
        {
            get { return _lapsLed; }
            set
            {
                if (_lapsLed != value)
                {
                    _lapsLed = value;
                    OnPropertyChanged("LapsLed");
                }
            }
        }

        public int LapsCompleted
        {
            get { return _lapsCompleted; }
            set
            {
                if (_lapsCompleted != value)
                {
                    _lapsCompleted = value;
                    OnPropertyChanged("LapsCompleted");
                }
            }
        }

        public int PointsEarned
        {
            get { return _pointsEarned; }
            set
            {
                if (_pointsEarned != value)
                {
                    _pointsEarned = value;
                    OnPropertyChanged("PointsEarned");
                }
            }
        }

        public int PlayoffPointsEarned
        {
            get { return _playoffPointsEarned; }
            set
            {
                if (_playoffPointsEarned != value)
                {
                    _playoffPointsEarned = value;
                    OnPropertyChanged("PlayoffPointsEarned");
                }
            }
        }
    }
}
