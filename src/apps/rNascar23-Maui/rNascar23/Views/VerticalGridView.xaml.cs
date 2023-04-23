using rNascar23.ViewModels;

namespace rNascar23.Views;

public partial class VerticalGridView : ContentView
{
    VerticalGridViewModel _viewModel;

    public VerticalGridView()
    {
        InitializeComponent();

        _viewModel = new VerticalGridViewModel();

        BindingContext = _viewModel;
    }
}