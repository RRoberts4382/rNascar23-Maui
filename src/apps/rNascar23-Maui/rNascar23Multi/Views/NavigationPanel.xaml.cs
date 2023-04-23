namespace rNascar23Multi.Views;

public partial class NavigationPanel : ContentView
{
	public NavigationPanel()
	{
		InitializeComponent();
	}

    private void Exit_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}