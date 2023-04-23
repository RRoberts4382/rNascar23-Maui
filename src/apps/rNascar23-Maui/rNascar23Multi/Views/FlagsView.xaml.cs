using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class FlagsView : ContentView
{
    private FlagsViewModel _viewModel;

    public FlagsView(FlagsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }
}