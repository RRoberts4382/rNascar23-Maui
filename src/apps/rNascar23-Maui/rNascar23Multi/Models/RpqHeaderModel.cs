namespace rNascar23Multi.Models
{
    public class RpqHeaderModel : NotifyModel
    {
        private int _flagState;
        public int FlagState
        {
            get
            {
                return _flagState;
            }
            set
            {
                _flagState = value;
                OnPropertyChanged(nameof(FlagState));
            }
        }

        private string _eventName;
        public string EventName
        {
            get
            {
                return _eventName;
            }
            set
            {
                _eventName = value;
                OnPropertyChanged(nameof(EventName));
            }
        }

        private string _raceLapInfo;
        public string RaceLapInfo
        {
            get
            {
                return _raceLapInfo;
            }
            set
            {
                _raceLapInfo = value;
                OnPropertyChanged(nameof(RaceLapInfo));
            }
        }

        private string _stageLapInfo;
        public string StageLapInfo
        {
            get
            {
                return _stageLapInfo;
            }
            set
            {
                _stageLapInfo = value;
                OnPropertyChanged(nameof(StageLapInfo));
            }
        }
    }
}
