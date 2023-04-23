using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

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