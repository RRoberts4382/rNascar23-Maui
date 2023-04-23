using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class KeyMomentsView : ContentView
{
    private KeyMomentsViewModel _viewModel;

    public KeyMomentsView(KeyMomentsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        BindingContext = _viewModel;
    }
}