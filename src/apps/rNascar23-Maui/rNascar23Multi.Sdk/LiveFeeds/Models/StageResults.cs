using System.Collections.Generic;

namespace rNascar23Multi.Sdk.LiveFeeds.Models
{
    public class StageResults
    {
        public int StageNumber { get; set; }
        public IList<DriverStageResult> Results { get; set; } = new List<DriverStageResult>();
    }
}
