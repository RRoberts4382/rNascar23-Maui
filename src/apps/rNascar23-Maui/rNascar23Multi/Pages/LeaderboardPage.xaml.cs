using Microsoft.Extensions.Logging;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace rNascar23Multi.Pages;

public partial class LeaderboardPage : ContentPage, INotifyPropertyChanged
{
    ILogger<LeaderboardPage> _logger;
    private ILiveFeedRepository _liveFeedRepository;
    private UpdateNotificationHandler _updateHandler;
    RpqHeaderView _headerView;
    LeaderboardView _leaderboardView;
    HorizontalGridView _horizontalGridView;
    VerticalGridView _verticalGridView;

    private EventViewType _viewType;
    public EventViewType EventViewType
    {
        get => _viewType;
        set
        {
            _viewType = value;
            OnPropertyChanged(nameof(EventViewType));
        }
    }

    public LeaderboardPage()
    {
        InitializeComponent();

        EventViewType = EventViewType.Race;

        _liveFeedRepository = App.serviceProvider.GetService<ILiveFeedRepository>();

        _logger = App.serviceProvider.GetService<ILogger<LeaderboardPage>>();

        _updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

        _updateHandler.UpdateTimerElapsed += UpdateTimer_UpdateTimerElapsed;

        _updateHandler.UserSettingsUpdated += _updateHandler_UserSettingsUpdated;

        this.Loaded += LeaderboardPage_Loaded;

        BindingContext = this;
    }

    private void _updateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
    {
        OnPropertyChanged(nameof(EventViewType));
    }

#pragma warning disable VSTHRD100 // Avoid async void methods
    private async void LeaderboardPage_Loaded(object sender, EventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
    {
        _headerView = new RpqHeaderView();
        headerViewHolder.Children.Add(_headerView);

        _leaderboardView = new LeaderboardView();
        leaderboardViewHolder.Children.Add(_leaderboardView);

        _horizontalGridView = new HorizontalGridView(EventViewType);
        horizontalGridViewHolder.Children.Add(_horizontalGridView);

        _verticalGridView = new VerticalGridView(EventViewType);
        verticalGridViewHolder.Children.Add(_verticalGridView);

        await LoadModelsAsync();
    }

#pragma warning disable VSTHRD100 // Avoid async void methods
    private async void UpdateTimer_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
    {
        try
        {
            await LoadModelsAsync(e.SessionDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in LeaderboardPage:UpdateTimer_UpdateTimerElapsed");
        }
    }

    private async Task LoadModelsAsync()
    {
        await LoadModelsAsync(null);
    }
    private async Task LoadModelsAsync(RaceSessionDetails sessionDetails)
    {
        try
        {
            var liveFeed = await _liveFeedRepository.GetLiveFeedAsync();

            if (liveFeed != null)
            {
                if ((sessionDetails == null) || (int)liveFeed.SeriesId != sessionDetails.SeriesId ||
                    liveFeed.RaceId != sessionDetails.RaceId)
                {
                    _updateHandler.UpdateSessionDetails(new RaceSessionDetails()
                    {
                        SeriesId = (int)liveFeed.SeriesId,
                        RaceId = liveFeed.RaceId,
                        Year = DateTime.Now.Year
                    });
                }
            }

            UpdateModels(_leaderboardView.ViewModel.LeftModels, liveFeed.Vehicles.OrderBy(v => v.RunningPosition).Take(20));

            UpdateModels(_leaderboardView.ViewModel.RightModels, liveFeed.Vehicles.OrderBy(v => v.RunningPosition).Skip(20));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in LeaderboardPage:LoadModelsAsync");
        }
    }

    private void UpdateModels(ObservableCollection<LeaderboardModel> models, IEnumerable<Vehicle> vehicles)
    {
        var orderedVehicles = vehicles.OrderBy(v => v.RunningPosition).ToArray();

        for (int i = 0; i < orderedVehicles.Count(); i++)
        {
            LeaderboardModel model;

            if (models.Count > i)
                model = models[i];
            else
            {
                model = new LeaderboardModel();

                models.Add(model);
            }

            model.Position = orderedVehicles[i].RunningPosition;
            model.CarNumber = orderedVehicles[i].VehicleNumber;
            model.DriverName = orderedVehicles[i].Driver?.FullName;
            model.Manufacturer = orderedVehicles[i].VehicleManufacturer;
            model.Laps = orderedVehicles[i].LapsCompleted;
            model.ToLeader = orderedVehicles[i].Delta;
            // TODO Calculate ToNext
            model.ToNext = orderedVehicles[i].Delta;
            // TODO Speed versus time
            model.LastLap = orderedVehicles[i].LastLapSpeed;
            // TODO Speed versus time
            model.BestLap = orderedVehicles[i].BestLapSpeed;
            model.OnLap = orderedVehicles[i].BestLap;
            model.Status = (int)orderedVehicles[i].Status;
        }
    }

    private void OnRaceButtonClicked(object sender, EventArgs e)
    {
        SetViewType(EventViewType.Race);
    }

    private void OnQualifyingButtonClicked(object sender, EventArgs e)
    {
        SetViewType(EventViewType.Qualifying);
    }

    private void OnPracticeButtonClicked(object sender, EventArgs e)
    {
        SetViewType(EventViewType.Practice);
    }

    private void SetViewType(EventViewType viewType)
    {
        EventViewType = viewType;

        _horizontalGridView.ViewType = EventViewType;
        _verticalGridView.ViewType = EventViewType;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetNavBarIsVisible(this, false);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Shell.SetNavBarIsVisible(this, true);
    }
}