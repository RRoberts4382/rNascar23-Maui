using rNascar23Multi.ViewModels;
using System.Diagnostics;

namespace rNascar23Multi.Views;

public partial class GridSelectionView : ContentView
{
    private GridSelectionViewModel _viewModel;

    public GridSelectionView()
    {
        InitializeComponent();

        try
        {
            _viewModel = App.serviceProvider.GetService<GridSelectionViewModel>();

            BindingContext = _viewModel;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

    }

    public GridSelectionView(GridSelectionViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }
}