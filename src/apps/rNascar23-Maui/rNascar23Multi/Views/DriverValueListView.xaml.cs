using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class DriverValueListView : ContentView
{
    private DriverValueViewModel _viewModel;

    public DriverValueListView()
    {
        InitializeComponent();

        _viewModel = new DriverValueViewModel(GridViewTypes.None);

        BindingContext = _viewModel;
    }

    public DriverValueListView(GridViewTypes gridViewType)
    {
        InitializeComponent();

        _viewModel = new DriverValueViewModel(gridViewType);

        BindingContext = _viewModel;
    }

    public DriverValueListView(DriverValueViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
    }
}