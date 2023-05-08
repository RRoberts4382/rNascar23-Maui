namespace rNascar23Multi.Models
{
    public class KeyMomentsModel : NotifyModel
    {
        private int _index;
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        private int _noteId;
        public int NoteId
        {
            get
            {
                return _noteId;
            }
            set
            {
                _noteId = value;
                OnPropertyChanged(nameof(NoteId));
            }
        }

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

        private int _lap;
        public int Lap
        {
            get
            {
                return _lap;
            }
            set
            {
                _lap = value;
                OnPropertyChanged(nameof(Lap));
            }
        }

        private string _comments;
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }
    }
}
