using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;
using System.Diagnostics;

namespace rNascar23Multi.Views;

public partial class DriverValueListView : ContentView, INotifyUpdateTarget, IDisposable
{
    private DriverValueViewModel _viewModel;

    public DriverValueListView()
    {
        InitializeComponent();

        _viewModel = new DriverValueViewModel(GridViewTypes.None);

        BindingContext = _viewModel;
    }

    public DriverValueListView(GridViewTypes gridViewType)
    {
        InitializeComponent();

        _viewModel = new DriverValueViewModel(gridViewType);

        BindingContext = _viewModel;
    }

    public DriverValueListView(DriverValueViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
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

    ~DriverValueListView()
    {
        Debug.WriteLine($"********************************* DriverValueListView Disposed");
    }
}