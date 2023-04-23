using rNascar23.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;

namespace rNascar23.ViewModels
{
    public class LeaderboardViewModel : ObservableObject
    {
        private ObservableCollection<LeaderboardModel> _leftModels = new ObservableCollection<LeaderboardModel>();
        private ObservableCollection<LeaderboardModel> _rightModels = new ObservableCollection<LeaderboardModel>();

        public ObservableCollection<LeaderboardModel> LeftModels
        {
            get => _leftModels;
            set => SetProperty(ref _leftModels, value);
        }
        public ObservableCollection<LeaderboardModel> RightModels
        {
            get => _rightModels;
            set => SetProperty(ref _rightModels, value);
        }

        public LeaderboardViewModel()
        {
            LoadFromSource();
        }

        private void LoadFromSource()
        {
            for (int i = 0; i < 20; i++)
            {
                LeftModels.Add(new LeaderboardModel()
                {
                    Position = i+1,
                    CarNumber = i.ToString(),
                    DriverName = $"Driver #{i}"
                });
            }
            for (int i = 20; i < 40; i++)
            {
                RightModels.Add(new LeaderboardModel()
                {
                    Position = i + 1,
                    CarNumber = i.ToString(),
                    DriverName = $"Driver #{i}"
                });
            }
        }
    }
}