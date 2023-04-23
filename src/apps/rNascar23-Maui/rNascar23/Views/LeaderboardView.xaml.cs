using rNascar23.ViewModels;

namespace rNascar23.Views;

public partial class LeaderboardView : ContentView
{
    private LeaderboardViewModel _viewModel;

    public LeaderboardView()
    {
        InitializeComponent();

        _viewModel = new LeaderboardViewModel();

        BindingContext = _viewModel;
    }
}