using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Common;

namespace rNascar23Multi.ViewModels
{
    public partial class SeriesEventViewModel : ObservableObject, IDisposable
    {
        #region fields

        private ILogger<SeriesEventViewModel> _logger;

        #endregion

        #region properties

        private bool _isSelected = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private SeriesTypes _seriesId;
        public SeriesTypes SeriesId
        {
            get { return _seriesId; }
            set { SetProperty(ref _seriesId, value); }
        }

        private int _raceId;
        public int RaceId
        {
            get { return _raceId; }
            set { SetProperty(ref _raceId, value); }
        }

        private string _eventName;
        public string EventName
        {
            get { return _eventName; }
            set { SetProperty(ref _eventName, value); }
        }

        private string _track;
        public string Track
        {
            get { return _track; }
            set { SetProperty(ref _track, value); }
        }

        private string _distance;
        public string Distance
        {
            get { return _distance; }
            set { SetProperty(ref _distance, value); }
        }

        private TvTypes _tv;
        public TvTypes Tv
        {
            get { return _tv; }
            set { SetProperty(ref _tv, value); }
        }

        private RadioTypes _radio;
        public RadioTypes Radio
        {
            get { return _radio; }
            set { SetProperty(ref _radio, value); }
        }

        private SatelliteTypes _satelliteRadio;
        public SatelliteTypes SatelliteRadio
        {
            get { return _satelliteRadio; }
            set { SetProperty(ref _satelliteRadio, value); }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _time;
        public string Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        #endregion

        #region ctor

        public SeriesEventViewModel(
            ILogger<SeriesEventViewModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region IDisposable

        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _logger = null;
            }

            _disposed = true;
        }

        #endregion
    }
}