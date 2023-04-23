using rNascar23.ViewModels;

namespace rNascar23.Views;

public partial class KeyMomentsView : ContentView
{
    private KeyMomentsViewModel _viewModel;

    public KeyMomentsView()
	{
		InitializeComponent();

        _viewModel = new KeyMomentsViewModel();

        BindingContext = _viewModel;
    }
}