using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class RpqHeaderView : ContentView
{
    private RpqHeaderViewModel _viewModel;

    public RpqHeaderView()
	{
		InitializeComponent();

        _viewModel = new RpqHeaderViewModel();

        BindingContext = _viewModel.Model;
    }
}