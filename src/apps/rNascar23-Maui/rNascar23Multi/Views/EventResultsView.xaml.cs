using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class EventResultsView : ContentView
{
    private EventResultsViewModel _viewModel;

    public EventResultsView()
    {
        InitializeComponent();

        _viewModel = App.serviceProvider.GetRequiredService<EventResultsViewModel>();

        BindingContext = _viewModel;
    }

    public EventResultsView(EventResultsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }
}