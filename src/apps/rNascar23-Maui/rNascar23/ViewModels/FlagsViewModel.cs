using rNascar23.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;

namespace rNascar23.ViewModels
{
    public class FlagsViewModel : ObservableObject
    {
        private ObservableCollection<FlagsModel> _models = new ObservableCollection<FlagsModel>();

        public ObservableCollection<FlagsModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _listHeader = "Flags";
        public string ListHeader
        {
            get => _listHeader;
            set => SetProperty(ref _listHeader, value);
        }

        public FlagsViewModel()
        {
            LoadFromSource();
        }

        private void LoadFromSource()
        {
            Models.Add(new FlagsModel()
            {
                FlagState = 8,
                Lap = 0,
                Timestamp = DateTime.Now.AddMinutes(-60)
            });

            Models.Add(new FlagsModel()
            {
                FlagState = 1,
                Lap = 0,
                Timestamp = DateTime.Now.AddMinutes(-45)
            });

            Models.Add(new FlagsModel()
            {
                FlagState = 2,
                Lap = 12,
                CautionFor = "#1, #11 Wreck in turn 2",
                LuckyDog = "51",
                Timestamp = DateTime.Now.AddMinutes(-36)
            });

            Models.Add(new FlagsModel()
            {
                FlagState = 1,
                Lap = 18,
                Timestamp = DateTime.Now.AddMinutes(-22)
            });

            Models.Add(new FlagsModel()
            {
                FlagState = 4,
                Lap = 25,
                Timestamp = DateTime.Now.AddMinutes(-15)
            });

            Models.Add(new FlagsModel()
            {
                FlagState = 9,
                Lap = 25,
                Timestamp = DateTime.Now.AddMinutes(-12)
            });
        }
    }
}