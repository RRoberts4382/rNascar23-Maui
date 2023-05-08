using Microsoft.Extensions.Logging;
using rNascar23Multi.Converters;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class HorizontalGridView : ContentView, IDisposable
{
    #region fields

    private ILogger<HorizontalGridView> _logger;
    private GridSetViewModel _viewModel;
    private GridViewFactory _viewFactory;
    private UpdateNotificationHandler _updateHandler;

    #endregion

    #region properties

    private EventViewType _viewType;
    public EventViewType ViewType
    {
        get
        {
            return _viewType;
        }
        set
        {
            _viewType = value;

            if (_viewModel != null)
                _viewModel.ViewType = _viewType;
        }
    }

    #endregion

    #region ctor

    public HorizontalGridView(EventViewType viewType)
    {
        InitializeComponent();

        _logger = App.serviceProvider.GetRequiredService<ILogger<HorizontalGridView>>();
        _updateHandler = App.serviceProvider.GetRequiredService<UpdateNotificationHandler>();

        _viewType = viewType;

        _viewModel = App.serviceProvider.GetRequiredService<GridSetViewModel>();
        _viewModel.GridOrientation = GridOrientationType.Horizontal;
        _viewModel.ViewType = _viewType;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        _viewModel.Models.CollectionChanged += Models_CollectionChanged;

        _viewFactory = App.serviceProvider.GetRequiredService<GridViewFactory>();

        UpdateGrids();

        _updateHandler.UpdateTimerElapsed += UpdateHandler_UpdateTimerElapsed;
        _updateHandler.UserSettingsUpdated += UpdateHandler_UserSettingsUpdated;
    }

    #endregion

    #region private

    private async void UpdateHandler_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
    {
        try
        {
            foreach (var childGrid in gridLayout.Children.OfType<INotifyUpdateTarget>())
            {
                await childGrid.UpdateTimerElapsedAsync(e);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HorizontalGridView.UserSettingsUpdated");
        }
    }

    protected virtual void UpdateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
    {
        try
        {
            _viewModel.UserSettingsUpdated(e);

            foreach (var childGrid in gridLayout.Children.OfType<INotifySettingsChanged>())
            {
                childGrid.UserSettingsUpdated(e);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in HorizontalGridView.UserSettingsUpdated");
        }
    }

    private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateGrids();
    }

    private void Models_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateGrids();
    }

    private void UpdateGrids()
    {
        var grids = gridLayout.Children.OfType<IDisposable>().ToList();

        gridLayout.Children.Clear();

        foreach (var grid in grids)
        {
            try
            {
                grid.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        foreach (GridViewModel item in _viewModel.Models)
        {
            var view = _viewFactory.GetGridView(item.ViewType);

            gridLayout.Children.Add(view);
        }
    }

    #endregion

    private bool _disposed;
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _viewModel?.Dispose();

            _viewModel = null;
            _logger = null;
            _updateHandler = null;
        }

        _disposed = true;
    }
}