using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class EventActivitiesView : ContentView
{
    private EventActivitiesViewModel _viewModel;

    public EventActivitiesView()
    {
        InitializeComponent();

        _viewModel = App.serviceProvider.GetRequiredService<EventActivitiesViewModel>();

        BindingContext = _viewModel;
    }

    public EventActivitiesView(EventActivitiesViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }
}