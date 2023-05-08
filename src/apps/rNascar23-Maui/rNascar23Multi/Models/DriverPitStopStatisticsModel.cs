namespace rNascar23Multi.Models
{
    public class DriverPitStopStatisticsModel : NotifyModel
    {
        private int? _numberOfStops;
        public int? NumberOfStops
        {
            get { return _numberOfStops; }
            set { _numberOfStops = value; OnPropertyChanged(nameof(NumberOfStops)); }
        }

        private double? _avgTotalTime;
        public double? AverageTotalTime
        {
            get { return _avgTotalTime; }
            set { _avgTotalTime = value; OnPropertyChanged(nameof(AverageTotalTime)); }
        }

        private int? _avgTotalTimeRank;
        public int? AverageTotalTimeRank
        {
            get { return _avgTotalTimeRank; }
            set { _avgTotalTimeRank = value; OnPropertyChanged(nameof(AverageTotalTimeRank)); }
        }


        private double? _avgPitTime;
        public double? AveragePitTime
        {
            get { return _avgPitTime; }
            set { _avgPitTime = value; OnPropertyChanged(nameof(AveragePitTime)); }
        }

        private int? _avgPitTimeRank;
        public int? AveragePitTimeRank
        {
            get { return _avgPitTimeRank; }
            set { _avgPitTimeRank = value; OnPropertyChanged(nameof(AveragePitTimeRank)); }
        }


        private float? _avgInOutTime;
        public float? AverageInOutTime
        {
            get { return _avgInOutTime; }
            set { _avgInOutTime = value; OnPropertyChanged(nameof(AverageInOutTime)); }
        }

        private int? _avgInOutTimeRank;
        public int? AverageInOutTimeRank
        {
            get { return _avgInOutTimeRank; }
            set { _avgInOutTimeRank = value; OnPropertyChanged(nameof(AverageInOutTimeRank)); }
        }


        private double? _avgGainLoss;
        public double? AverageGainLoss
        {
            get { return _avgGainLoss; }
            set { _avgGainLoss = value; OnPropertyChanged(nameof(AverageGainLoss)); }
        }

        private int? _avgGainLossRank;
        public int? AverageGainLossRank
        {
            get { return _avgGainLossRank; }
            set { _avgGainLossRank = value; OnPropertyChanged(nameof(AverageGainLossRank)); }
        }
    }
}
