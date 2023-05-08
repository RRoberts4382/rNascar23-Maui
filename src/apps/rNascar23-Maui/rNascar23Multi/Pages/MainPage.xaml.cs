using rNascar23Multi.Views;

namespace rNascar23Multi.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var mainView = new MainView();

        mainViewHolder.Children.Add(mainView);
    }
}