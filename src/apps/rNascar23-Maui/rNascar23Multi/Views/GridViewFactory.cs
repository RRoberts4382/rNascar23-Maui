using rNascar23Multi.Models;

namespace rNascar23Multi.Views
{
    internal class GridViewFactory
    {
        public IView GetGridView(GridViewTypes gridViewType)
        {
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
                case GridViewTypes.Points:
                case GridViewTypes.StagePoints:
                case GridViewTypes.DriverRatings:
                    {
                        var view = new DriverValueView(gridViewType);
                        view.MinimumHeightRequest = 150;
                        view.Margin = new Thickness(2);
                        return view;
                    }
                case GridViewTypes.Flags:
                    {
                        var view = new FlagsView();
                        view.Margin = new Thickness(2);
                        return view;
                    }
                case GridViewTypes.KeyMoments:
                    {
                        var view = new KeyMomentsView();
                        view.Margin = new Thickness(2);
                        return view;
                    }
                default:
                    {
                        throw new NotImplementedException($"Unsupported grid view type: {gridViewType}");
                    }
            }
        }
    }
}
