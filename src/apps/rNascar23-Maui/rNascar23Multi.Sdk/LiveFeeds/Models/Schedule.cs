using rNascar23Multi.Sdk.Common;
using System;

namespace rNascar23Multi.Sdk.LiveFeeds.Models
{
    public class Schedule
    {
        public string EventName { get; set; }
        public string Notes { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public RunTypes RunType { get; set; }
    }
}
