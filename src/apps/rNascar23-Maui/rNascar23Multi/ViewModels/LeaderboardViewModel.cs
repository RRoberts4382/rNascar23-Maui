using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
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
        }
    }
}