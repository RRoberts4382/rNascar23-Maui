using rNascar23.ViewModels;

namespace rNascar23.Views;

public partial class HorizontalGridView : ContentView
{
    HorizontalGridViewModel _viewModel;

    public HorizontalGridView()
    {
        InitializeComponent();

        _viewModel = new HorizontalGridViewModel();

        BindingContext = _viewModel;
    }
}