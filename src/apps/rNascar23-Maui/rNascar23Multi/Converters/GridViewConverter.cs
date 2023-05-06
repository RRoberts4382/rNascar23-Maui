using Microsoft.Extensions.Logging;
using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;
using rNascar23Multi.Views;
using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class GridViewConverter : IValueConverter
    {
        static ILogger<GridViewConverter> _logger;

        public static GridViewConverter Instance { get; } = new();

        public GridViewConverter()
        {
            _logger = App.serviceProvider.GetRequiredService<ILogger<GridViewConverter>>();
        }

        public object Convert(object value)
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
                        var view = new DriverValueListView(gridViewType);
                        view.MinimumHeightRequest = 150;
                        view.Margin = new Thickness(2);
                        return view;
                    }

                case GridViewTypes.Points:
                case GridViewTypes.StagePoints:
                    {
                        var view = new PositionDriverValueListView(gridViewType);
                        view.MinimumHeightRequest = 150;
                        view.Margin = new Thickness(2);
                        return view;
                    }
                case GridViewTypes.Flags:
                    {
                        var viewModel = App.serviceProvider.GetService<FlagsViewModel>();
                        var view = new FlagsView(viewModel);
                        view.Margin = new Thickness(2);
                        return view;
                    }
                case GridViewTypes.KeyMoments:
                    {
                        var viewModel = App.serviceProvider.GetService<KeyMomentsViewModel>();
                        var view = new KeyMomentsView(viewModel);
                        view.Margin = new Thickness(2);
                        return view;
                    }
                default:
                    {
                        var view = new DriverValueListView(gridViewType);
                        view.Margin = new Thickness(2);
                        return view;
                    }
            }
        }

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
                        var viewModel = App.serviceProvider.GetService<FlagsViewModel>();
                        return new FlagsView(viewModel);
                    }
                case GridViewTypes.KeyMoments:
                    {
                        return App.serviceProvider.GetService<KeyMomentsView>();
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