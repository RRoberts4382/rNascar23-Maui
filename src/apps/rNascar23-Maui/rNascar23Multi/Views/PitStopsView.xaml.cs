using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class PitStopsView : ContentView
{
    private PitStopsViewModel _viewModel;
    private UpdateNotificationHandler _updateHandler;

    public PitStopsView()
    {
        InitializeComponent();

        _updateHandler = App.serviceProvider.GetRequiredService<UpdateNotificationHandler>();

        _updateHandler.UpdateTimerElapsed += _updateHandler_UpdateTimerElapsed;

        _viewModel = App.serviceProvider.GetRequiredService<PitStopsViewModel>();

        BindingContext = _viewModel;
    }

    public PitStopsView(
        PitStopsViewModel viewModel,
        UpdateNotificationHandler updateHandler)
    {
        InitializeComponent();

        _updateHandler = updateHandler;

        _updateHandler.UpdateTimerElapsed += _updateHandler_UpdateTimerElapsed;

        _updateHandler.UserSettingsUpdated += _updateHandler_UserSettingsUpdated;

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    private async void _updateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
    {
        await _viewModel.UserSettingsUpdatedAsync();
    }

    private async void _updateHandler_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
    {
        await _viewModel.UpdateTimerElapsedAsync(e);
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedModel = e.SelectedItem as PitStopsAllDriversModel;

        if (selectedModel == null)
            return;

        foreach (var model in _viewModel.AllDriversPitStops.Where(m => m.IsSelected == true))
        {
            model.IsSelected = false;
        }

        selectedModel.IsSelected = true;

        await _viewModel?.UpdateSelectedDriverAsync(selectedModel);
    }

    private async void UpdateByLaps_Clicked(object sender, EventArgs e)
    {
        await _viewModel.UpdateByLapsAsync();
    }
    private async void UpdateByCaution_Clicked(object sender, EventArgs e)
    {
        await _viewModel.UpdateByCautionAsync();
    }
    private async void UpdateByDriver_Clicked(object sender, EventArgs e)
    {
        await _viewModel.UpdateByDriverAsync();
    }
    private async void AllPitStops_Clicked(object sender, EventArgs e)
    {
        await _viewModel.AllPitStopsAsync();
    }

    
}