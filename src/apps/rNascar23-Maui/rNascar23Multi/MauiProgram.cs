using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using rNascar23.Sdk;
using rNascar23Multi.Logic;
using rNascar23Multi.Settings;
using rNascar23Multi.ViewModels;
using Serilog;
using Serilog.Events;

namespace rNascar23Multi
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            SetupSerilog();

            builder
               .UseMauiApp<App>()
               .UseMauiCommunityToolkit()
               .RegisterViewModels()
               .RegisterListBuilders()
               .RegisterSdk()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });

            builder.Services.AddSettings();

            builder.Services.AddSingleton<UpdateNotificationHandler>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddLogging(logging => logging.AddSerilog(dispose: true));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void SetupSerilog()
        {
            var flushInterval = new TimeSpan(0, 0, 1);
            var file = Path.Combine(FileSystem.AppDataDirectory, "rNascar23Multi.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.File(file, flushToDiskInterval: flushInterval, encoding: System.Text.Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 22)
                .WriteTo.Debug()
                .CreateLogger();
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<SchedulesViewModel>();
            mauiAppBuilder.Services.AddTransient<PitStopsViewModel>();
            mauiAppBuilder.Services.AddTransient<EventDetailsViewModel>();
            mauiAppBuilder.Services.AddTransient<EventResultsViewModel>();
            mauiAppBuilder.Services.AddTransient<EventActivitiesViewModel>();
            mauiAppBuilder.Services.AddTransient<GridSelectionViewModel>();
            mauiAppBuilder.Services.AddTransient<SettingsViewModel>();
            mauiAppBuilder.Services.AddTransient<SeriesEventViewModel>();
            mauiAppBuilder.Services.AddTransient<DriverValueViewModel>();
            mauiAppBuilder.Services.AddTransient<GridSetViewModel>();
            mauiAppBuilder.Services.AddTransient<FlagsViewModel>();
            mauiAppBuilder.Services.AddTransient<KeyMomentsViewModel>();
            mauiAppBuilder.Services.AddTransient<LeaderboardViewModel>();
            mauiAppBuilder.Services.AddTransient<RpqHeaderViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterListBuilders(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<ListBuilderFactory>();
            mauiAppBuilder.Services.AddTransient<LapLeaderListBuilder>();
            mauiAppBuilder.Services.AddTransient<FastestLapsListBuilder>();
            mauiAppBuilder.Services.AddTransient<MoversListBuilder>();
            mauiAppBuilder.Services.AddTransient<FallersListBuilder>();
            mauiAppBuilder.Services.AddTransient<DriverPointsListBuilder>();
            mauiAppBuilder.Services.AddTransient<StagePointsListBuilder>();



            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterSdk(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services
                .AddrNascar23Sdk();

            return mauiAppBuilder;
        }
    }
}