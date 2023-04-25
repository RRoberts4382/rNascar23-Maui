using AutoMapper;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using rNascar23Multi.Logic;
using rNascar23Multi.Sdk.Service.Flags;
using rNascar23Multi.Sdk.Service.LapTimes;
using rNascar23Multi.Sdk.Service.LiveFeeds;
using rNascar23Multi.Sdk.Service.LoopData;
using rNascar23Multi.Sdk.Service.Media;
using rNascar23Multi.Sdk.Service.PitStops;
using rNascar23Multi.Sdk.Service.Points;
using rNascar23Multi.Sdk.Service.Schedules;
using rNascar23Multi.ViewModels;
using rNascar23Multi.Views;
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
               .RegisterViews()
               .RegisterSdk()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });

            builder.Services.AddSingleton<UpdateNotificationHandler>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //builder.Logging.AddSerilog(dispose: true);
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
            mauiAppBuilder.Services.AddTransient<DriverValueViewModel>();
            mauiAppBuilder.Services.AddTransient<FlagsViewModel>();
            mauiAppBuilder.Services.AddTransient<HorizontalGridViewModel>();
            mauiAppBuilder.Services.AddTransient<KeyMomentsViewModel>();
            mauiAppBuilder.Services.AddTransient<LeaderboardViewModel>();
            mauiAppBuilder.Services.AddTransient<PositionDriverValueViewModel>();
            mauiAppBuilder.Services.AddTransient<RpqHeaderViewModel>();
            mauiAppBuilder.Services.AddTransient<VerticalGridViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<rNascar23Main>();

            mauiAppBuilder.Services.AddTransient<DriverValueListView>();
            mauiAppBuilder.Services.AddTransient<FlagsView>();
            mauiAppBuilder.Services.AddTransient<HorizontalGridView>();
            mauiAppBuilder.Services.AddTransient<KeyMomentsView>();
            mauiAppBuilder.Services.AddTransient<LeaderboardView>();
            mauiAppBuilder.Services.AddTransient<NavigationPanel>();
            mauiAppBuilder.Services.AddTransient<PositionDriverValueListView>();
            mauiAppBuilder.Services.AddTransient<RpqHeaderView>();
            mauiAppBuilder.Services.AddTransient<VerticalGridView>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterSdk(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services
                .AddFlagState()
                .AddSchedules()
                .AddLiveFeed()
                .AddLapTimes()
                .AddMedia()
                .AddPoints()
                .AddLoopData()
                .AddPitStops();

            return mauiAppBuilder;
        }
    }
}