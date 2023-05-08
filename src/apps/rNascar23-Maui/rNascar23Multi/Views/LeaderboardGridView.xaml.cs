using rNascar23Multi.Logic;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class LeaderboardGridView : ContentView, INotifyUpdateTarget, INotifySettingsChanged, IDisposable
{
    #region fields

    private LeaderboardGridViewModel _viewModel;

    #endregion

    #region ctor

    public LeaderboardGridView()
    {
        InitializeComponent();

        _viewModel = App.serviceProvider.GetService<LeaderboardGridViewModel>();

        BindingContext = _viewModel;
    }

    #endregion

    #region public

    public void UserSettingsUpdated(SettingsModel settings)
    {
        _viewModel.UserSettingsUpdated(settings);
    }

    public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
    {
        await _viewModel.UpdateTimerElapsedAsync(e);
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
            _viewModel?.Dispose();
        }

        _disposed = true;
    }

    #endregion
}