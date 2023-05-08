using rNascar23.Sdk.Common;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Ports;

namespace rNascar23Multi.Logic
{
    internal class ListBuilderFactory
    {
        private ISettingsRepository _settingsRepository;

        public ListBuilderFactory(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
        }

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
                case GridViewTypes.DriverRatings:
                    {
                        return App.serviceProvider.GetRequiredService<DriverRatingsListBuilder>();
                    }
                case GridViewTypes.Best5Laps:
                case GridViewTypes.Best10Laps:
                case GridViewTypes.Best15Laps:
                case GridViewTypes.Last5Laps:
                case GridViewTypes.Last10Laps:
                case GridViewTypes.Last15Laps:
                    {
                        var listBuilder = App.serviceProvider.GetRequiredService<LastBestLapsListBuilder>();

                        listBuilder.GridViewType = gridViewType;

                        var userSettings = _settingsRepository.GetSettings();

                        listBuilder.SpeedTimeType = userSettings.UseMph ? SpeedTimeType.MPH : SpeedTimeType.Seconds;

                        return listBuilder;
                    }
                case GridViewTypes.None:
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }
    }
}
