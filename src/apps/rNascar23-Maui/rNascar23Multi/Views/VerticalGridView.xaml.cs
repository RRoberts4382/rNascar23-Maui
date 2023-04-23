using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

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