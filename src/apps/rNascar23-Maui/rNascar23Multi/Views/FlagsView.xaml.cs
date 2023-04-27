using Microsoft.Extensions.Logging;
using rNascar23Multi.Logic;
using rNascar23Multi.ViewModels;
using System.Diagnostics;

namespace rNascar23Multi.Views;

public partial class FlagsView : ContentView, INotifyUpdateTarget, IDisposable
{
    private ILogger<FlagsView> _logger;
    private FlagsViewModel _viewModel;

    public FlagsView(FlagsViewModel viewModel)
    {
        _logger = App.serviceProvider.GetRequiredService<ILogger<FlagsView>>();

        InitializeComponent();

        _viewModel = viewModel;

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

            _logger = null;
        }
        // free native resources if there are any.

        _disposed = true;
    }

    ~FlagsView()
    {
        Debug.WriteLine("********************************* FlagsView Disposed");
    }
}