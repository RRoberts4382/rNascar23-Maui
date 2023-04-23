using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

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