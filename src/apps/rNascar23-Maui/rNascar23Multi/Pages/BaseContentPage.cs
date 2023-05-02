namespace rNascar23Multi.Pages;

public class BaseContentPage : ContentPage
{
	public BaseContentPage()
	{
		
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //Shell.SetNavBarIsVisible(this, false);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        //Shell.SetNavBarIsVisible(this, true);
    }
}