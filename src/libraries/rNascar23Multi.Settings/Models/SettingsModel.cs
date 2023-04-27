using System.ComponentModel;

namespace rNascar23Multi.Settings.Models
{
    public class SettingsModel : INotifyPropertyChanged
    {
        #region properties

        private string _dataDirectory;
        public string DataDirectory
        {
            get
            {
                return _dataDirectory;
            }
            set
            {
                _dataDirectory = value;
                OnPropertyChanged(nameof(DataDirectory));
            }
        }

        private string _logDirectory;
        public string LogDirectory
        {
            get
            {
                return _logDirectory;
            }
            set
            {
                _logDirectory = value;
                OnPropertyChanged(nameof(LogDirectory));
            }
        }

        private bool _autoUpdateEnabledOnStart = true;
        public bool AutoUpdateEnabledOnStart
        {
            get
            {
                return _autoUpdateEnabledOnStart;
            }
            set
            {
                _autoUpdateEnabledOnStart = value;
                OnPropertyChanged(nameof(AutoUpdateEnabledOnStart));
            }
        }

        private bool _useMph;
        public bool UseMph
        {
            get
            {
                return _useMph;
            }
            set
            {
                _useMph = value;
                OnPropertyChanged(nameof(UseMph));
            }
        }

        private bool _useDarkTheme;
        public bool UseDarkTheme
        {
            get
            {
                return _useDarkTheme;
            }
            set
            {
                _useDarkTheme = value;
                OnPropertyChanged(nameof(UseDarkTheme));
            }
        }

        private bool _displayTimeDifference;
        public bool DisplayTimeDifference
        {
            get
            {
                return _displayTimeDifference;
            }
            set
            {
                _displayTimeDifference = value;
                OnPropertyChanged(nameof(DisplayTimeDifference));
            }
        }

        private int? _dataDelayInSeconds=0;
        public int? DataDelayInSeconds
        {
            get
            {
                return _dataDelayInSeconds;
            }
            set
            {
                _dataDelayInSeconds = value;
                OnPropertyChanged(nameof(DataDelayInSeconds));
            }
        }

        private int? _audioDelayInSeconds=0;
        public int? AudioDelayInSeconds
        {
            get
            {
                return _audioDelayInSeconds;
            }
            set
            {
                _audioDelayInSeconds = value;
                OnPropertyChanged(nameof(AudioDelayInSeconds));
            }
        }

        private IList<string> _favoriteDrivers = new List<string>();
        public IList<string> FavoriteDrivers
        {
            get
            {
                return _favoriteDrivers;
            }
            set
            {
                _favoriteDrivers = value;
                OnPropertyChanged(nameof(FavoriteDrivers));
            }
        }

        private IList<int> _raceViewBottomGrids = new List<int>();
        public IList<int> RaceViewBottomGrids
        {
            get
            {
                return _raceViewBottomGrids;
            }
            set
            {
                _raceViewBottomGrids = value;
                OnPropertyChanged(nameof(RaceViewBottomGrids));
            }
        }

        private IList<int> _raceViewRightGrids = new List<int>();
        public IList<int> RaceViewRightGrids
        {
            get
            {
                return _raceViewRightGrids;
            }
            set
            {
                _raceViewRightGrids = value;
                OnPropertyChanged(nameof(RaceViewRightGrids));
            }
        }

        private IList<int> _qualifyingViewBottomGrids = new List<int>();
        public IList<int> QualifyingViewBottomGrids
        {
            get
            {
                return _qualifyingViewBottomGrids;
            }
            set
            {
                _qualifyingViewBottomGrids = value;
                OnPropertyChanged(nameof(QualifyingViewBottomGrids));
            }
        }

        private IList<int> _qualifyingViewRightGrids = new List<int>();
        public IList<int> QualifyingViewRightGrids
        {
            get
            {
                return _qualifyingViewRightGrids;
            }
            set
            {
                _qualifyingViewRightGrids = value;
                OnPropertyChanged(nameof(QualifyingViewRightGrids));
            }
        }

        private IList<int> _practiceViewBottomGrids = new List<int>();
        public IList<int> PracticeViewBottomGrids
        {
            get
            {
                return _practiceViewBottomGrids;
            }
            set
            {
                _practiceViewBottomGrids = value;
                OnPropertyChanged(nameof(PracticeViewBottomGrids));
            }
        }

        private IList<int> _practiceViewRightGrids = new List<int>();
        public IList<int> PracticeViewRightGrids
        {
            get
            {
                return _practiceViewRightGrids;
            }
            set
            {
                _practiceViewRightGrids = value;
                OnPropertyChanged(nameof(PracticeViewRightGrids));
            }
        }

        #endregion

        #region events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
