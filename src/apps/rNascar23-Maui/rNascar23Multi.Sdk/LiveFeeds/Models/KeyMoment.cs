using rNascar23Multi.Sdk.Common;

namespace rNascar23Multi.Sdk.LiveFeeds.Models
{
    public class KeyMoment
    {
        public int LapNumber { get; set; }
        public FlagColors FlagState { get; set; }
        public string Note { get; set; }
        public int NoteId { get; set; }
        public IList<int> DriverIds { get; set; } = new List<int>();
    }
}
