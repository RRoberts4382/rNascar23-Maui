using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

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