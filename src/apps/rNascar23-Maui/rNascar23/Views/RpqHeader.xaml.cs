using rNascar23.ViewModels;

namespace rNascar23.Views;

public partial class RpqHeader : ContentView
{
    private RpqHeaderViewModel _viewModel;

    public RpqHeader()
	{
		InitializeComponent();

        _viewModel = new RpqHeaderViewModel();

        BindingContext = _viewModel.Model;
    }
}