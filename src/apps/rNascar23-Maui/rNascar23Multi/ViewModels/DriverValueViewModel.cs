using rNascar23Multi.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public partial class DriverValueViewModel : ObservableObject
    {
        private ObservableCollection<DriverValueModel> _models = new ObservableCollection<DriverValueModel>();

        public ObservableCollection<DriverValueModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _listHeader = "Driver/Value Grid";
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

        public DriverValueViewModel()
        {
            LoadFromSource();
        }

        public DriverValueViewModel(GridViewTypes gridViewType)
        {
            switch (gridViewType)
            {
                case GridViewTypes.FastestLaps:
                    {
                        ListHeader = "Fastest Laps";
                        HeaderTextColor = Colors.Black;
                        HeaderBackgroundColor = Colors.LightSteelBlue;
                        break;
                    }
                case GridViewTypes.LapLeaders:
                    {
                        ListHeader = "Lap Leaders";
                        HeaderTextColor = Colors.White;
                        HeaderBackgroundColor = Colors.DarkBlue;
                        break;
                    }
                case GridViewTypes.Movers:
                    {
                        ListHeader = "Movers";
                        HeaderBackgroundColor = Colors.Green;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.Fallers:
                    {
                        ListHeader = "Fallers";
                        HeaderBackgroundColor = Colors.Red;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.StagePoints:
                    {
                        ListHeader = "Stage Points";
                        HeaderBackgroundColor = Colors.Blue;
                        HeaderTextColor = Colors.White;
                        break;
                    }
                case GridViewTypes.Best5Laps:
                case GridViewTypes.Best10Laps:
                case GridViewTypes.Best15Laps:
                    {
                        ListHeader = gridViewType.ToString();
                        HeaderBackgroundColor = Colors.LightSkyBlue;
                        HeaderTextColor = Colors.Black;
                        break;
                    }
                case GridViewTypes.Last5Laps:
                case GridViewTypes.Last10Laps:
                case GridViewTypes.Last15Laps:
                    {
                        ListHeader = gridViewType.ToString();
                        HeaderBackgroundColor = Colors.Blue;
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
            Models.Add(new DriverValueModel()
            {
                Driver = "Jim Smith",
                Value = 0.123F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Frank Rizzo",
                Value = 2.472F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Parker Kligerman",
                Value = 3.472F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Joey Gase",
                Value = 4.472F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Austin Hill",
                Value = 5.472F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Dale Earnhardt",
                Value = 0.123F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Red Byron",
                Value = 2.472F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Curtis Turner",
                Value = 3.472F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Junior Johnson",
                Value = 4.472F
            });

            Models.Add(new DriverValueModel()
            {
                Driver = "Jeff Gordon",
                Value = 5.472F
            });
        }
    }
}
