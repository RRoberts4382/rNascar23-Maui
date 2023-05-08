using rNascar23Multi.ViewModels;

namespace rNascar23Multi.Views;

public partial class SettingsView : ContentView
{
    private SettingsViewModel _viewModel;

    public SettingsView()
    {
        InitializeComponent();

        _viewModel = App.serviceProvider.GetService<SettingsViewModel>();

        BindingContext = _viewModel;
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _viewModel.UpdateTheme();
    }
}