using AutoMapper;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using rNascar23Multi.Sdk.Service.Flags;
using rNascar23Multi.Views;
using rNascar23Multi.ViewModels;

namespace rNascar23Multi
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp
           .CreateBuilder()
           .UseMauiApp<App>()
           .UseMauiCommunityToolkitMediaElement()
           .RegisterViewModels()
           .RegisterViews()
           .RegisterSdk()
           .ConfigureFonts(fonts =>
           {
               fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
               fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
           });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
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
            mauiAppBuilder.Services.AddFlagState();

            return mauiAppBuilder;
        }
    }
}