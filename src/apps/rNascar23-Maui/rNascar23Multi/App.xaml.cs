namespace rNascar23Multi
{
    public partial class App : Application
    {
        public static IServiceProvider serviceProvider { get; set; }

        public App(IServiceProvider sp)
        {
            InitializeComponent();

            serviceProvider = sp;

            MainPage = new AppShell();
        }
    }
}