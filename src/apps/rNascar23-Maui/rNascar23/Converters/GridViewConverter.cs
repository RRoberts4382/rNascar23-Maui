using rNascar23.Models;
using rNascar23.ViewModels;
using rNascar23.Views;
using System.Globalization;

namespace rNascar23.Converters
{
    public class GridViewConverter : IValueConverter
    {
        public static GridViewConverter Instance { get; } = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridViewTypes gridViewType = (GridViewTypes)value;

            switch (gridViewType)
            {
                case GridViewTypes.FastestLaps:
                case GridViewTypes.LapLeaders:
                case GridViewTypes.Movers:
                case GridViewTypes.Fallers:
                case GridViewTypes.Best5Laps:
                case GridViewTypes.Best10Laps:
                case GridViewTypes.Best15Laps:
                case GridViewTypes.Last5Laps:
                case GridViewTypes.Last10Laps:
                case GridViewTypes.Last15Laps:
                    {
                        return new DriverValueListView(gridViewType);
                    }

                case GridViewTypes.Points:
                case GridViewTypes.StagePoints:
                    {
                        return new PositionDriverValueListView(gridViewType);
                    }
                case GridViewTypes.Flags:
                    {
                        return new FlagsView();
                    }
                case GridViewTypes.KeyMoments:
                    {
                        return new KeyMomentsView();
                    }
                default:
                    {
                        return new DriverValueViewModel(gridViewType);
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}