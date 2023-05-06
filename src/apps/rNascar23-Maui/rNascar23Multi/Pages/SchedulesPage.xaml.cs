using rNascar23Multi.Logic;
using rNascar23Multi.Views;

namespace rNascar23Multi.Pages;

public partial class SchedulesPage : ContentPage
{
    private readonly SchedulesView _schedulesView;
    private readonly UpdateNotificationHandler _updateHandler;

    public SchedulesPage()
    {
        InitializeComponent();

        _schedulesView = new SchedulesView();

        schedulesViewHolder.Children.Add(_schedulesView);
    }
}