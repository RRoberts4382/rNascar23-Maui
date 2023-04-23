using rNascar23Multi.Logic;
using System.Diagnostics;

namespace rNascar23Multi;

public partial class rNascar23Main : ContentPage
{
    IDispatcherTimer timer;

    public rNascar23Main(UpdateNotificationHandler updateTimer)
    {
        InitializeComponent();

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(10000);
        timer.Tick += (s, e) =>
        {
            Debug.WriteLine("timer elapsed");

            updateTimer.BroadcastTimerFired();
        };

        timer.Start();
    }
}