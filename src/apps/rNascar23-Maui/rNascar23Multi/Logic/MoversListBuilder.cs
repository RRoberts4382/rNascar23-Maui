using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal class MoversListBuilder : MoversFallersListBuilderBase, IListBuilder<DriverValueModel>
    {
        public MoversListBuilder(ILapTimesRepository lapTimeRepository, IMoversFallersService moversFallersService)
            : base(lapTimeRepository, moversFallersService)
        {
        }

        internal override void UpdateData(ObservableCollection<DriverValueModel> models, LapTimeData liveFeed, IList<PositionChange> changes)
        {
            var driverValues = changes.
                OrderByDescending(c => c.ChangeSinceFlagChange).
                Where(c => c.ChangeSinceFlagChange > 0).
                Select((c, i) => new DriverValueModel()
                {
                    Position = i,
                    Driver = c.Driver,
                    Value = c.ChangeSinceFlagChange
                }).
                Take(5).
                ToList();

            ModelUpdater.UpdateModels(models, driverValues);
        }
    }
}
