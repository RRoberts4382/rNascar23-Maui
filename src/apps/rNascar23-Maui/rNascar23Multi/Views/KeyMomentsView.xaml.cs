using Microsoft.Extensions.DependencyInjection;
using rNascar23Multi.Logic;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class KeyMomentsView : ContentView, INotifyUpdateTarget, IDisposable
{
    private KeyMomentsViewModel _viewModel;

    public KeyMomentsView()
    {
        InitializeComponent();

        _viewModel = App.serviceProvider.GetRequiredService<KeyMomentsViewModel>();

        BindingContext = _viewModel;
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