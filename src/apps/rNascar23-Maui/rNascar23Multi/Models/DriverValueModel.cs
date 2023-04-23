namespace rNascar23Multi.Models
{
    public class DriverValueModel : NotifyModel
    {
        private string _driver;
        public string Driver
        {
            get
            {
                return _driver;
            }
            set
            {
                _driver = value;
                OnPropertyChanged(nameof(Driver));
            }
        }

        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
}
