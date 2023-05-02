namespace rNascar23Multi.Views;

public partial class TestView : ContentView
{
    HorizontalGridView _horizontalGridView;

    public TestView()
    {
        InitializeComponent();

        _horizontalGridView = App.serviceProvider.GetRequiredService<HorizontalGridView>();

        currentContentViewHolder.Children.Add(_horizontalGridView);
    }

    private void OnPracticeButtonClicked(object sender, EventArgs e)
    {
        //var horizontalGridView = App.serviceProvider.GetRequiredService<HorizontalGridView>();
        //var horizontalGridView = new HorizontalGridView();

        _horizontalGridView.ViewType = Models.EventViewType.Practice;

        //currentContentViewHolder.Children.Clear();

        //currentContentViewHolder.Children.Add(horizontalGridView);
    }

    private void OnQualifyingButtonClicked(object sender, EventArgs e)
    {
        //var horizontalGridView = App.serviceProvider.GetRequiredService<HorizontalGridView>();
        //var horizontalGridView = new HorizontalGridView();

        _horizontalGridView.ViewType = Models.EventViewType.Qualifying;

        //currentContentViewHolder.Children.Clear();

        //currentContentViewHolder.Children.Add(horizontalGridView);
    }

    private void OnRaceButtonClicked(object sender, EventArgs args)
    {
        //var horizontalGridView = App.serviceProvider.GetRequiredService<HorizontalGridView>();
        //var horizontalGridView = new HorizontalGridView();

        _horizontalGridView.ViewType = Models.EventViewType.Race;

        //currentContentViewHolder.Children.Clear();

        //currentContentViewHolder.Children.Add(horizontalGridView);
    }

}