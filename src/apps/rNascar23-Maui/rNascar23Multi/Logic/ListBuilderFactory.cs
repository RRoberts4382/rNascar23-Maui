using rNascar23Multi.Models;

namespace rNascar23Multi.Logic
{
    internal class ListBuilderFactory
    {
        public IListBuilder<DriverValueModel> GetListBuilder(GridViewTypes gridViewType)
        {
            switch (gridViewType)
            {
                case GridViewTypes.FastestLaps:
                    {
                        return App.serviceProvider.GetRequiredService<FastestLapsListBuilder>();
                    }
                case GridViewTypes.LapLeaders:
                    {
                        return App.serviceProvider.GetRequiredService<LapLeaderListBuilder>();
                    }
                case GridViewTypes.Movers:
                    {
                        return App.serviceProvider.GetRequiredService<MoversListBuilder>();
                    }
                case GridViewTypes.Fallers:
                    {
                        return App.serviceProvider.GetRequiredService<FallersListBuilder>();
                    }
                case GridViewTypes.Points:
                    {
                        return App.serviceProvider.GetRequiredService<DriverPointsListBuilder>();
                    }
                case GridViewTypes.StagePoints:
                    {
                        return App.serviceProvider.GetRequiredService<StagePointsListBuilder>();
                    }
                case GridViewTypes.Best5Laps:
                case GridViewTypes.Best10Laps:
                case GridViewTypes.Best15Laps:
                case GridViewTypes.Last5Laps:
                case GridViewTypes.Last10Laps:
                case GridViewTypes.Last15Laps:
                case GridViewTypes.None:
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }
    }
}
