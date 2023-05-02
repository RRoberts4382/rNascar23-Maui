using rNascar23Multi.Logic;
using rNascar23Multi.ViewModels;
using System.Diagnostics;

namespace rNascar23Multi.Views;

public partial class KeyMomentsView : ContentView, INotifyUpdateTarget, IDisposable
{
    private KeyMomentsViewModel _viewModel;

    public KeyMomentsView(KeyMomentsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        BindingContext = _viewModel;
    }

    public async Task UserSettingsUpdatedAsync()
    {
        await _viewModel.UserSettingsUpdatedAsync();
    }

    public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
    {
        await _viewModel.UpdateTimerElapsedAsync(e);
    }

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
        }
        // free native resources if there are any.

        _disposed = true;
    }

    ~KeyMomentsView()
    {
        Debug.WriteLine("********************************* KeyMomentsView Disposed");
    }
}