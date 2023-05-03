using Microsoft.Extensions.Logging;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23Multi.Converters;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class VerticalGridView : ContentView, IDisposable
{
    #region fields

    private ILogger<VerticalGridView> _logger;
    private GridSetViewModel _viewModel;
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

    public VerticalGridView(EventViewType viewType)
    {
        InitializeComponent();

        _logger = App.serviceProvider.GetRequiredService<ILogger<VerticalGridView>>();
        _updateHandler = App.serviceProvider.GetRequiredService<UpdateNotificationHandler>();

        _viewType = viewType;

        _viewModel = App.serviceProvider.GetRequiredService<GridSetViewModel>();
        _viewModel.GridOrientation = GridOrientationType.Vertical;
        _viewModel.ViewType = _viewType;

        _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        _viewModel.Models.CollectionChanged += Models_CollectionChanged;

        UpdateGrids();

        _updateHandler.UpdateTimerElapsed += _updateHandler_UpdateTimerElapsed;
        _updateHandler.UserSettingsUpdated += _updateHandler_UserSettingsUpdated;
    }

    #endregion

    #region private

    private async void _updateHandler_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
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
            _logger.LogError(ex, "Error in VerticalGridView.UserSettingsUpdated");
        }
    }

    protected virtual async void _updateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
    {
        try
        {
            foreach (var childGrid in gridLayout.Children.OfType<INotifyUpdateTarget>())
            {
                await childGrid.UserSettingsUpdatedAsync();
            }

            await _viewModel.UserSettingsUpdatedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in VerticalGridView.UserSettingsUpdated");
        }
    }

    private void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            var view = (IView)GridViewConverter.Instance.Convert(item.ViewType);

            gridLayout.Children.Add(view);
        }
    }

    #endregion

    #region IDisposable

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
            if (_viewModel != null)
                _viewModel.Dispose();

            _viewModel = null;
            _logger = null;
            _updateHandler = null;
        }
        // free native resources if there are any.
    }

    #endregion
}