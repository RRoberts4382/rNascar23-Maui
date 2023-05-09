using rNascar23.Sdk.Common;

namespace rNascar23Multi.Models
{
    public class LeaderboardGridModel : NotifyModel
    {
        private int _position;
        public int Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        private string _carNumber;
        public string CarNumber
        {
            get { return _carNumber; }
            set
            {
                _carNumber = value;
                OnPropertyChanged(nameof(CarNumber));
            }
        }

        private string _driverName;
        public string DriverName
        {
            get { return _driverName; }
            set
            {
                _driverName = value;
                OnPropertyChanged(nameof(DriverName));
            }
        }

        private string _manufacturer;
        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                _manufacturer = value;
                OnPropertyChanged(nameof(Manufacturer));
            }
        }

        private int _laps;
        public int Laps
        {
            get { return _laps; }
            set
            {
                _laps = value;
                OnPropertyChanged(nameof(Laps));
            }
        }

        private float _toLeader;
        public float ToLeader
        {
            get { return _toLeader; }
            set
            {
                _toLeader = value;
                OnPropertyChanged(nameof(ToLeader));
            }
        }

        private float _toNext;
        public float ToNext
        {
            get { return _toNext; }
            set
            {
                _toNext = value;
                OnPropertyChanged(nameof(ToNext));
            }
        }

        private float _lastLap;
        public float LastLap
        {
            get { return _lastLap; }
            set
            {
                _lastLap = value;
                OnPropertyChanged(nameof(LastLap));
            }
        }

        private float _bestLap;
        public float BestLap
        {
            get { return _bestLap; }
            set
            {
                _bestLap = value;
                OnPropertyChanged(nameof(BestLap));
            }
        }

        private int _onLap;
        public int OnLap
        {
            get { return _onLap; }
            set
            {
                _onLap = value;
                OnPropertyChanged(nameof(OnLap));
            }
        }

        private int? _lastPit;
        public int? LastPit
        {
            get { return _lastPit; }
            set
            {
                _lastPit = value;
                OnPropertyChanged(nameof(LastPit));
            }
        }

        private VehicleStatusTypes _status;
        public VehicleStatusTypes Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private bool _isFastestLapOfRace;
        public bool IsFastestLapOfRace
        {
            get { return _isFastestLapOfRace; }
            set
            {
                _isFastestLapOfRace = value;
                OnPropertyChanged(nameof(IsFastestLapOfRace));
            }
        }

        private bool _isDriverFastestLapOfRace;
        public bool IsDriverFastestLapOfRace
        {
            get { return _isDriverFastestLapOfRace; }
            set
            {
                _isDriverFastestLapOfRace = value;
                OnPropertyChanged(nameof(IsDriverFastestLapOfRace));
            }
        }

        private bool _isFastestLastLap;
        public bool IsFastestLastLap
        {
            get { return _isFastestLastLap; }
            set
            {
                _isFastestLastLap = value;
                OnPropertyChanged(nameof(IsFastestLastLap));
            }
        }

        private bool _isFavoriteDriver;
        public bool IsFavoriteDriver
        {
            get { return _isFavoriteDriver; }
            set
            {
                _isFavoriteDriver = value;
                OnPropertyChanged(nameof(Position));
            }
        }
    }
}
