using rNascar23Multi.Logic;
using rNascar23Multi.Views;
using System.Diagnostics;

namespace rNascar23Multi
{
    public partial class AppShell : Shell
    {
        IDispatcherTimer _timer;

        public AppShell()
        {
            InitializeComponent();

            var updatehandler = App.serviceProvider.GetRequiredService<UpdateNotificationHandler>();

            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10000);
            _timer.Tick += (s, e) =>
            {
                //Debug.WriteLine("timer elapsed");

                updatehandler.BroadcastTimerFired();
            };

            _timer.Start();
        }
    }
}