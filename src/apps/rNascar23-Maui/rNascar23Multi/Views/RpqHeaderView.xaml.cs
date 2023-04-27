using rNascar23Multi.Logic;
using rNascar23Multi.ViewModels;
using System.Diagnostics;

namespace rNascar23Multi.Views;

public partial class RpqHeaderView : ContentView, INotifyUpdateTarget, IDisposable
{
    private RpqHeaderViewModel _viewModel;

    public RpqHeaderView()
    {
        InitializeComponent();

        _viewModel = new RpqHeaderViewModel();

        BindingContext = _viewModel.Model;
    }

    public RpqHeaderView(RpqHeaderViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        BindingContext = _viewModel.Model;
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

            //_logger = null;
        }
        // free native resources if there are any.

        _disposed = true;
    }

    ~RpqHeaderView()
    {
        Debug.WriteLine("********************************* RpqHeaderView Disposed");
    }
}