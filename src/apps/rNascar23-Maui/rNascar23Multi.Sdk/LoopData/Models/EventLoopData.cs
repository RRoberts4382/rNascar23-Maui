using rNascar23Multi.Sdk.Common;
using System.Collections.Generic;

namespace rNascar23Multi.Sdk.LoopData.Models
{
    public class EventLoopData
    {
        public int RaceId { get; set; }
        public string RaceName { get; set; }
        // TODO enum
        public SeriesTypes SeriesId { get; set; }
        public int ScheduledLaps { get; set; }
        public int ActualLaps { get; set; }
        public IList<DriverLoopData> Drivers { get; set; }
    }
}
