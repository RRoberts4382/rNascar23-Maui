using Microsoft.Extensions.Logging;
using rNascar23.Sdk.Common;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class SchedulesView : ContentView
{
    #region fields

    private readonly ILogger<SchedulesView> _logger;
    private readonly SchedulesViewModel _viewModel;
    private readonly UpdateNotificationHandler _updateHandler;

    #endregion

    #region properties

    private ScheduleType _scheduleType;
    public ScheduleType ScheduleType
    {
        get => _scheduleType;
        set
        {
            _scheduleType = value;
            _viewModel.ScheduleType = _scheduleType;
            OnPropertyChanged(nameof(ScheduleType));
        }
    }

    private ActivitiesResultsTypes _activitiesResultsType;
    public ActivitiesResultsTypes ActivitiesResultsType
    {
        get => _activitiesResultsType;
        set
        {
            _activitiesResultsType = value;
            _viewModel.ActivitiesResultsType = _activitiesResultsType;
        }
    }

    #endregion

    #region properties

    public SchedulesView()
    {
        InitializeComponent();

        _logger = App.serviceProvider.GetService<ILogger<SchedulesView>>();

        _viewModel = App.serviceProvider.GetService<SchedulesViewModel>();

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;

        BindingContext = _viewModel;

        _updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

        _updateHandler.UserSettingsUpdated += UpdateHandler_UserSettingsUpdated;
    }

    #endregion

    #region private - event handlers

    private void UpdateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
    {
        OnPropertyChanged(nameof(ScheduleType));
    }

    private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ScheduleType))
        {
            await UpdateSeriesEventListAsync();
        }
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedModel = e.SelectedItem as SeriesEventModel;

        if (selectedModel == null)
            return;

        foreach (var model in _viewModel.Models.Where(m => m.IsSelected == true))
        {
            model.IsSelected = false;
        }

        selectedModel.IsSelected = true;

        await UpdateActivitiesResultsAsync(selectedModel);
    }

    /* activities / results */
    private async void OnEventActivitiesButtonClicked(object sender, EventArgs e)
    {
        try
        {
            ActivitiesResultsType = ActivitiesResultsTypes.Activities;

            await DisplayActivitiesResultsAsync(_viewModel.Selected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error setting activity/results type to Activities");
        }
    }
    private async void OnEventResultsButtonClicked(object sender, EventArgs e)
    {
        try
        {
            ActivitiesResultsType = ActivitiesResultsTypes.Results;

            await DisplayActivitiesResultsAsync(_viewModel.Selected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Error setting activity/results type to Results");
        }
    }

    /* schedule types */
    private void OnTodayButtonClicked(object sender, EventArgs e)
    {
        SetScheduleType(ScheduleType.Today);
    }
    private void OnThisWeekButtonClicked(object sender, EventArgs e)
    {
        SetScheduleType(ScheduleType.ThisWeek);
    }
    private void OnNextWeekButtonClicked(object sender, EventArgs e)
    {
        SetScheduleType(ScheduleType.NextWeek);
    }
    private void OnCupButtonClicked(object sender, EventArgs e)
    {
        SetScheduleType(ScheduleType.Cup);
    }
    private void OnXfinityButtonClicked(object sender, EventArgs e)
    {
        SetScheduleType(ScheduleType.Xfinity);
    }
    private void OnTrucksButtonClicked(object sender, EventArgs e)
    {
        SetScheduleType(ScheduleType.Trucks);
    }
    private void OnAllButtonClicked(object sender, EventArgs e)
    {
        SetScheduleType(ScheduleType.All);
    }

    #endregion

    #region private

    /* series events */
    private async Task UpdateSeriesEventListAsync()
    {
        await _viewModel.LoadSeriesEventModelsAsync();
    }

    /* activities / results */
    private async Task UpdateActivitiesResultsAsync(SeriesEventModel selectedModel)
    {
        if (selectedModel.Timestamp >= DateTime.Now.Date)
        {
            ActivitiesResultsType = ActivitiesResultsTypes.Activities;

            eventResultsButton.IsEnabled = false;
        }
        else
        {
            ActivitiesResultsType = ActivitiesResultsTypes.Results;

            eventResultsButton.IsEnabled = true;
        }

        await DisplayActivitiesResultsAsync(selectedModel);
    }

    private async Task DisplayActivitiesResultsAsync(SeriesEventModel selectedModel)
    {
        if (ActivitiesResultsType == ActivitiesResultsTypes.Activities)
        {
            await DisplayActivitiesViewAsync(selectedModel.RaceId);
        }
        else
        {
            await DisplayResultsViewAsync(selectedModel.SeriesId, selectedModel.RaceId);

            await DisplayDetailsViewAsync(selectedModel.RaceId);
        }
    }

    private async Task DisplayActivitiesViewAsync(int raceId)
    {
        eventDetailsGrid.Children.Clear();

        activitiesResultsGrid.Children.Clear();

        var viewModel = App.serviceProvider.GetRequiredService<EventActivitiesViewModel>();

        await viewModel.LoadModelsAsync(raceId);

        var activityView = new EventActivitiesView(viewModel);

        activitiesResultsGrid.Children.Add(activityView);
    }

    private async Task DisplayResultsViewAsync(SeriesTypes seriesId, int raceId)
    {
        activitiesResultsGrid.Children.Clear();

        var viewModel = App.serviceProvider.GetRequiredService<EventResultsViewModel>();

        await viewModel.LoadEventResultsAsync(seriesId, raceId);

        var activityView = new EventResultsView(viewModel);

        activitiesResultsGrid.Children.Add(activityView);
    }

    private async Task DisplayDetailsViewAsync(int raceId)
    {
        eventDetailsGrid.Children.Clear();

        var viewModel = App.serviceProvider.GetRequiredService<EventDetailsViewModel>();

        await viewModel.LoadEventDetailsAsync(raceId);

        var activityView = new EventDetailsView(viewModel);

        eventDetailsGrid.Children.Add(activityView);
    }

    /* schedule types */
    private void SetScheduleType(ScheduleType scheduleType)
    {
        _viewModel.ScheduleType = scheduleType;
    }

    #endregion
}