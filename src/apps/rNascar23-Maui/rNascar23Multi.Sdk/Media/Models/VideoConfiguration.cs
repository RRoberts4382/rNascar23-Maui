namespace rNascar23Multi.Sdk.Media.Models
{
    public class VideoConfiguration
    {
        public bool Live { get; set; }
        public int RaceID { get; set; }
        public int DefaultDriverID { get; set; }
        public IList<VideoComponent> VideoComponents { get; set; } = new List<VideoComponent>();
    }
}
