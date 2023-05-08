using Microsoft.Extensions.Logging;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;

namespace rNascar23Multi.Views;

public partial class MainView : ContentView
{
    private readonly ILogger<MainView> _logger;
    private readonly UpdateNotificationHandler _updateHandler;
    private RpqHeaderView _headerView;
    private LeaderboardGridView _leaderboardView;
    private HorizontalGridView _horizontalGridView;
    private VerticalGridView _verticalGridView;
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

    public MainView()
	{
		InitializeComponent(); 

        EventViewType = EventViewType.Race;

        _logger = App.serviceProvider.GetService<ILogger<MainView>>();

        _updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

        _updateHandler.UpdateTimerElapsed += UpdateHandler_UpdateTimerElapsed;

        _updateHandler.UserSettingsUpdated += UpdateHandler_UserSettingsUpdated;

        _headerView = new RpqHeaderView();
        headerViewHolder.Children.Add(_headerView);

        _leaderboardView = new LeaderboardGridView();
        leaderboardViewHolder.Children.Add(_leaderboardView);

        _horizontalGridView = new HorizontalGridView(EventViewType);
        horizontalGridViewHolder.Children.Add(_horizontalGridView);

        _verticalGridView = new VerticalGridView(EventViewType);
        verticalGridViewHolder.Children.Add(_verticalGridView);

        BindingContext = this;
    }

    private void UpdateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
    {
        try
        {
            _leaderboardView.UserSettingsUpdated(e);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MainView:UpdateHandler_UserSettingsUpdated");
        }
    }

    private async void UpdateHandler_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
    {
        try
        {
            await _headerView.UpdateTimerElapsedAsync(e);
            await _leaderboardView.UpdateTimerElapsedAsync(e);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MainView:UpdateTimer_UpdateTimerElapsed");
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
}