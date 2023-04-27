namespace rNascar23Multi.Models
{
    public class GridSelectionModel : NotifyModel
    {
        private string _gridName;
        public string GridName
        {
            get
            {
                if (String.IsNullOrEmpty(_gridName))
                    return GridViewType.ToString();
                else
                    return _gridName;
            }
            set
            {
                _gridName = value;
                OnPropertyChanged(nameof(GridName));
            }
        }

        private GridViewTypes _gridViewType;
        public GridViewTypes GridViewType
        {
            get
            {
                return _gridViewType;
            }
            set
            {
                _gridViewType = value;
                OnPropertyChanged(nameof(GridViewType));
            }
        }

        private int _gridIndex;
        public int GridIndex
        {
            get
            {
                return _gridIndex;
            }
            set
            {
                _gridIndex = value;
                OnPropertyChanged(nameof(GridIndex));
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
