using CommunityToolkit.Maui;
using FixerApp.Data;
using FixerApp.Pages;
using FixerApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace FixerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<IDatabase, Database>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransientWithShellRoute<AddFixerJobPage, AddFixerJobVM>(nameof(AddFixerJobPage));
            builder.Services.AddTransientWithShellRoute<KalenderPage, KalenderPageVM>(nameof(KalenderPage));
            builder.Services.AddTransientWithShellRoute<SpecificDatePage, SpecificDateVM>(nameof(SpecificDatePage));
            builder.Services.AddTransient<MainPageVM>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
