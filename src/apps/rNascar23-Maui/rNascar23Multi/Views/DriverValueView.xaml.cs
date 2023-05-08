using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.ViewModels;
using System.Diagnostics;

namespace rNascar23Multi.Views;

public partial class DriverValueView : ContentView, INotifyUpdateTarget, INotifySettingsChanged, IDisposable
{
    private DriverValueViewModel _viewModel;

    public DriverValueView(GridViewTypes gridViewType)
    {
        InitializeComponent();

        _viewModel = new DriverValueViewModel(gridViewType);

        BindingContext = _viewModel;
    }

    public void UserSettingsUpdated(SettingsModel settings)
    {
        _viewModel.UserSettingsUpdated(settings);
    }

    public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
    {
        await _viewModel.UpdateTimerElapsedAsync(e);
    }

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
            _viewModel?.Dispose();
        }

        _disposed = true;
    }

    #endregion
}