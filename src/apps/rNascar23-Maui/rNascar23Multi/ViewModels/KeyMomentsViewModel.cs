using rNascar23Multi.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public class KeyMomentsViewModel : ObservableObject
    {
        private ObservableCollection<KeyMomentsModel> _models = new ObservableCollection<KeyMomentsModel>();

        public ObservableCollection<KeyMomentsModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _listHeader = "Key Moments";
        public string ListHeader
        {
            get => _listHeader;
            set => SetProperty(ref _listHeader, value);
        }

        public KeyMomentsViewModel()
        {
            LoadFromSource();
        }

        private void LoadFromSource()
        {
            Models.Add(new KeyMomentsModel()
            {
                FlagState = 8,
                Comments = "Track is hot",
                Lap = 0,
                Timestamp = DateTime.Now.AddMinutes(-60)
            });

            Models.Add(new KeyMomentsModel()
            {
                FlagState = 1,
                Comments = "Green Flag!",
                Lap = 0,
                Timestamp = DateTime.Now.AddMinutes(-45)
            });

            Models.Add(new KeyMomentsModel()
            {
                FlagState = 2,
                Lap = 12,
                Comments = "#1, #11 are involved in a big wreck in turn 2, everyone else gets away cleanly",
                Timestamp = DateTime.Now.AddMinutes(-36)
            });

            Models.Add(new KeyMomentsModel()
            {
                FlagState = 1,
                Comments = "Green Flag",
                Lap = 18,
                Timestamp = DateTime.Now.AddMinutes(-22)
            });

            Models.Add(new KeyMomentsModel()
            {
                FlagState = 4,
                Comments = "White Flag",
                Lap = 85,
                Timestamp = DateTime.Now.AddMinutes(-15)
            });

            Models.Add(new KeyMomentsModel()
            {
                FlagState = 9,
                Comments = "Track is cold",
                Lap = 99,
                Timestamp = DateTime.Now.AddMinutes(-12)
            });
        }
    }
}