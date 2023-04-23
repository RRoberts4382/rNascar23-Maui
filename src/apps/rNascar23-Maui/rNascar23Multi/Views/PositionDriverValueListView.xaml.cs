using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class PositionDriverValueListView : ContentView
{
    private PositionDriverValueViewModel _viewModel;

    public PositionDriverValueListView()
    {
        InitializeComponent();

        _viewModel = new PositionDriverValueViewModel();

        BindingContext = _viewModel;
    }

    public PositionDriverValueListView(GridViewTypes gridViewType)
    {
        InitializeComponent();

        _viewModel = new PositionDriverValueViewModel(gridViewType);

        BindingContext = _viewModel;
    }

    public PositionDriverValueListView(PositionDriverValueViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
    }
}