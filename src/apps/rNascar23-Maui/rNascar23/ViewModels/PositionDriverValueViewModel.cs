using rNascar23.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;

namespace rNascar23.ViewModels
{
    public partial class PositionDriverValueViewModel : ObservableObject
    {
        private ObservableCollection<PositionDriverValueModel> _models = new ObservableCollection<PositionDriverValueModel>();

        public ObservableCollection<PositionDriverValueModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _listHeader = "Position/Driver/Value Grid";
        public string ListHeader
        {
            get => _listHeader;
            set => SetProperty(ref _listHeader, value);
        }

        private Color _headerTextColor = Colors.Black;
        public Color HeaderTextColor
        {
            get => _headerTextColor;
            set => SetProperty(ref _headerTextColor, value);
        }

        private Color _headerBackgroundColor = Colors.White;
        public Color HeaderBackgroundColor
        {
            get => _headerBackgroundColor;
            set => SetProperty(ref _headerBackgroundColor, value);
        }

        public PositionDriverValueViewModel()
        {
            LoadFromSource();
        }

        public PositionDriverValueViewModel(GridViewTypes gridViewType)
        {
            switch (gridViewType)
            {
                case GridViewTypes.Points:
                    {
                        ListHeader = "Driver Points";
                        HeaderBackgroundColor = Colors.Orange;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.StagePoints:
                    {
                        ListHeader = "Stage Points";
                        HeaderBackgroundColor = Colors.Teal;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.None:
                default:
                    {
                        ListHeader = gridViewType.ToString();
                        HeaderBackgroundColor = Colors.Black;
                        HeaderTextColor = Colors.White;
                        break;
                    }
            }

            LoadFromSource();
        }

        protected virtual void LoadFromSource()
        {
            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Jim Smith",
                Value = 0.123F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Frank Rizzo",
                Value = 2.472F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Parker Kligerman",
                Value = 3.472F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Joey Gase",
                Value = 4.472F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Austin Hill",
                Value = 5.472F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Dale Earnhardt",
                Value = 0.123F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Red Byron",
                Value = 2.472F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Curtis Turner",
                Value = 3.472F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Junior Johnson",
                Value = 4.472F
            });

            Models.Add(new PositionDriverValueModel()
            {
                Driver = "Jeff Gordon",
                Value = 5.472F
            });

            for (int i = 0; i < Models.Count; i++)
            {
                Models[i].Position = i;
            }
        }
    }
}
