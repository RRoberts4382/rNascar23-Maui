using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class LeaderboardView : ContentView
{
    private LeaderboardViewModel _viewModel;

    public LeaderboardView()
    {
        InitializeComponent();

        _viewModel = App.serviceProvider.GetService<LeaderboardViewModel>();

        BindingContext = _viewModel;
    }

    public LeaderboardView(LeaderboardViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        BindingContext = _viewModel;
    }
}