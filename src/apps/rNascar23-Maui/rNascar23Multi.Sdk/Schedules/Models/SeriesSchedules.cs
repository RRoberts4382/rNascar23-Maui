namespace rNascar23Multi.Sdk.Schedules.Models
{
    public class SeriesSchedules
    {
        public IList<SeriesEvent> CupSeries { get; set; } = new List<SeriesEvent>();
        public IList<SeriesEvent> XfinitySeries { get; set; } = new List<SeriesEvent>();
        public IList<SeriesEvent> TruckSeries { get; set; } = new List<SeriesEvent>();
        public IEnumerable<SeriesEvent> AllSeries
        {
            get
            {
                return CupSeries.Concat(XfinitySeries).Concat(TruckSeries);
            }
        }
    }
}
