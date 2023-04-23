namespace rNascar23Multi.Models
{
    public class PositionDriverValueModel : DriverValueModel
    {
        private int _position;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
    }
}
