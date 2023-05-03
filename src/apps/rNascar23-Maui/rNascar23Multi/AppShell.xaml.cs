using rNascar23Multi.Logic;
using rNascar23Multi.Resources.Styles;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.Settings.Ports;
using rNascar23Multi.Views;
using System.Diagnostics;

namespace rNascar23Multi
{
    public partial class AppShell : Shell
    {
        IDispatcherTimer _timer;
        ISettingsRepository _settingsRepository;

        public AppShell()
        {
            InitializeComponent();

            _settingsRepository = App.serviceProvider.GetRequiredService<ISettingsRepository>();

            var settings = _settingsRepository.GetSettings();

            UpdateTheme(settings);

            var updatehandler = App.serviceProvider.GetRequiredService<UpdateNotificationHandler>();

            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10000);
            _timer.Tick += (s, e) =>
            {
                updatehandler.BroadcastTimerFired();
            };

            if (settings.AutoUpdateEnabledOnStart)
                _timer.Start();
        }

        public void UpdateTheme(SettingsModel model)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                if (model.UseDarkTheme)
                {
                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new LightTheme());
                }
            }
        }
    }
}