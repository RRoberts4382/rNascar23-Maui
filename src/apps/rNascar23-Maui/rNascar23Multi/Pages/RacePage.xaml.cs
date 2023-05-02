using rNascar23Multi.Logic;
using System.Diagnostics;

namespace rNascar23Multi.Pages;

public partial class RacePage : BaseContentPage
{
    IDispatcherTimer timer;

    public RacePage(UpdateNotificationHandler updateTimer)
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