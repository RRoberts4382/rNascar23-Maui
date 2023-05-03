namespace rNascar23Multi.Models
{
    public class EventDetailsModel : NotifyModel
    {
        private string _winner;
        public string Winner
        {
            get { return _winner; }
            set
            {
                if (_winner != value)
                {
                    _winner = value;
                    OnPropertyChanged("Winner");
                }
            }
        }

        private int _leaders;
        public int Leaders
        {
            get { return _leaders; }
            set
            {
                if (_leaders != value)
                {
                    _leaders = value;
                    OnPropertyChanged("Leaders");
                }
            }
        }

        private int _cautions;
        public int Cautions
        {
            get { return _cautions; }
            set
            {
                if (_cautions != value)
                {
                    _cautions = value;
                    OnPropertyChanged("Cautions");
                }
            }
        }

        private string _poleWinner;
        public string PoleWinner
        {
            get { return _poleWinner; }
            set
            {
                if (_poleWinner != value)
                {
                    _poleWinner = value;
                    OnPropertyChanged("PoleWinner");
                }
            }
        }

        private float _averageSpeed;
        public float AverageSpeed
        {
            get { return _averageSpeed; }
            set
            {
                if (_averageSpeed != value)
                {
                    _averageSpeed = value;
                    OnPropertyChanged("AverageSpeed");
                }
            }
        }

        private string _raceTime;
        public string RaceTime
        {
            get { return _raceTime; }
            set
            {
                if (_raceTime != value)
                {
                    _raceTime = value;
                    OnPropertyChanged("RaceTime");
                }
            }
        }

        private int _leadChanges;
        public int LeadChanges
        {
            get { return _leadChanges; }
            set
            {
                if (_leadChanges != value)
                {
                    _leadChanges = value;
                    OnPropertyChanged("LeadChanges");
                }
            }
        }

        private int _cautionLaps;
        public int CautionLaps
        {
            get { return _cautionLaps; }
            set
            {
                if (_cautionLaps != value)
                {
                    _cautionLaps = value;
                    OnPropertyChanged("CautionLaps");
                }
            }
        }

        private float _poleSpeed;
        public float PoleSpeed
        {
            get { return _poleSpeed; }
            set
            {
                if (_poleSpeed != value)
                {
                    _poleSpeed = value;
                    OnPropertyChanged("PoleSpeed");
                }
            }
        }

        private string _marginOfVictory;
        public string MarginOfVictory
        {
            get { return _marginOfVictory; }
            set
            {
                if (_marginOfVictory != value)
                {
                    _marginOfVictory = value;
                    OnPropertyChanged("MarginOfVictory");
                }
            }
        }

        private int _carsInField;
        public int CarsInField
        {
            get { return _carsInField; }
            set
            {
                if (_carsInField != value)
                {
                    _carsInField = value;
                    OnPropertyChanged("CarsInField");
                }
            }
        }

        private string _summary;
        public string Summary
        {
            get { return _summary; }
            set
            {
                if (_summary != value)
                {
                    _summary = value;
                    OnPropertyChanged("Summary");
                }
            }
        }
    }
}
