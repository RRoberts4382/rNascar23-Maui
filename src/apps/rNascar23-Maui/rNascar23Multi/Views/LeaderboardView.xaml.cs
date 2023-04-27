using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class LeaderboardView : ContentView
{
    public LeaderboardViewModel ViewModel { get; set; }

    public LeaderboardView()
    {
        InitializeComponent();

        ViewModel = App.serviceProvider.GetService<LeaderboardViewModel>();

        BindingContext = ViewModel;
    }

    public LeaderboardView(LeaderboardViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        BindingContext = ViewModel;
    }
}