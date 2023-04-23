using rNascar23.ViewModels;

namespace rNascar23.Views;

public partial class FlagsView : ContentView
{
    private FlagsViewModel _viewModel;

    public FlagsView()
    {
        InitializeComponent();

        _viewModel = new FlagsViewModel();

        BindingContext = _viewModel;
    }
}