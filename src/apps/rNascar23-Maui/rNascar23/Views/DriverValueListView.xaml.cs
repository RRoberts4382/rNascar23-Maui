using rNascar23.Models;
using rNascar23.ViewModels;

namespace rNascar23.Views;

public partial class DriverValueListView : ContentView
{
    private DriverValueViewModel _viewModel;
    
    public DriverValueListView()
    {
        InitializeComponent();

        _viewModel = new DriverValueViewModel();

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