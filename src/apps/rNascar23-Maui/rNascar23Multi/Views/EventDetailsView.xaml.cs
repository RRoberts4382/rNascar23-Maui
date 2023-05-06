using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class EventDetailsView : ContentView
{
    private EventDetailsViewModel _viewModel;

    public EventDetailsView()
    {
        InitializeComponent();

        _viewModel = App.serviceProvider.GetRequiredService<EventDetailsViewModel>();

        BindingContext = _viewModel.Model;
    }

    public EventDetailsView(EventDetailsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel.Model;
    }
}