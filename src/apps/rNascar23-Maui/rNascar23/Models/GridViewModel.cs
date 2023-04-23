namespace rNascar23.Models
{
    internal class GridViewModel : NotifyModel
    {
        private GridViewTypes _gridViewType;
        public GridViewTypes ViewType
        {
            get
            {
                return _gridViewType;
            }
            set
            {
                _gridViewType = value;
                OnPropertyChanged(nameof(ViewType));
            }
        }
    }
}
