namespace rNascar23Multi.Models
{
    public class FavoriteDriverSelectionModel : NotifyModel
    {
        private string _driverName;
        public string DriverName
        {
            get
            {
                return _driverName;
            }
            set
            {
                _driverName = value;
                OnPropertyChanged(nameof(DriverName));
            }
        }

        private bool _selected;
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
    }
}
