namespace rNascar23Multi.Models
{
    public class PitStopsAllDriversModel : NotifyModel
    {
        private int _index;
        public int Index
        {
            get { return _index; }
            set { _index = value; OnPropertyChanged(nameof(Index)); }
        }

        private string _driver;
        public string Driver
        {
            get { return _driver; }
            set { _driver = value; OnPropertyChanged(nameof(Driver)); }
        }

        private string _carNumber;
        public string CarNumber
        {
            get { return _carNumber; }
            set { _carNumber = value; OnPropertyChanged(nameof(CarNumber)); }
        }

        private string _tires;
        public string Tires
        {
            get { return _tires; }
            set { _tires = value; OnPropertyChanged(nameof(Tires)); }
        }

        private int _lap;
        public int Lap
        {
            get { return _lap; }
            set { _lap = value; OnPropertyChanged(nameof(Lap)); }
        }

        private float _totalTime;
        public float TotalTime
        {
            get { return _totalTime; }
            set { _totalTime = value; OnPropertyChanged(nameof(TotalTime)); }
        }

        private float _pitTime;
        public float PitTime
        {
            get { return _pitTime; }
            set { _pitTime = value; OnPropertyChanged(nameof(PitTime)); }
        }

        private int _flag;
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; OnPropertyChanged(nameof(Flag)); }
        }

        private int _positionIn;
        public int PositionIn
        {
            get { return _positionIn; }
            set { _positionIn = value; OnPropertyChanged(nameof(PositionIn)); }
        }

        private int _positionOut;
        public int PositionOut
        {
            get { return _positionOut; }
            set { _positionOut = value; OnPropertyChanged(nameof(PositionOut)); }
        }

        private int _positionDelta;
        public int PositionDelta
        {
            get { return _positionDelta; }
            set { _positionDelta = value; OnPropertyChanged(nameof(PositionDelta)); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }
    }
}
