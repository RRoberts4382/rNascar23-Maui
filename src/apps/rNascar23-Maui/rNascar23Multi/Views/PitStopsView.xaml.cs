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

        _updateHandler.UpdateTimerElapsed += UpdateHandler_UpdateTimerElapsed;

        _updateHandler.UserSettingsUpdated += UpdateHandler_UserSettingsUpdated;

        _viewModel = App.serviceProvider.GetRequiredService<PitStopsViewModel>();

        BindingContext = _viewModel;
    }

    private void UpdateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
    {
        _viewModel.UserSettingsUpdated(e);
    }

    private async void UpdateHandler_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
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
        await _viewModel.FilterByLapsAsync();
    }

    private async void UpdateByCaution_Clicked(object sender, EventArgs e)
    {
        await _viewModel.FilterByCautionAsync();
    }

    private async void UpdateByDriver_Clicked(object sender, EventArgs e)
    {
        await _viewModel.FilterByDriverAsync();
    }

    private async void AllPitStops_Clicked(object sender, EventArgs e)
    {
        await _viewModel.AllPitStopsAsync();
    }
}