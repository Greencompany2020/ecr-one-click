using CommunityToolkit.Maui;
using EcrOneClick.DI;
using EcrOneClick.Infrastructure.Database.Configurations;
using Microsoft.Extensions.Logging;
using SQLite;

namespace EcrOneClick;

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
                fonts.AddFont("Jaldi-Bold.ttf", "JaldiBold");
                fonts.AddFont("Jaldi-Regular.ttf", "JaldiRegular");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.LoadServices();
        builder.LoadViewModels();
        builder.LoadValidators();

        builder.Services.AddSingleton<SQLiteConnection>((provider) =>
            new SQLiteConnection(SqliteConfiguration.DatabasePath, SqliteConfiguration.Flags));

        builder.LoadRepositories();
        builder.LoadRepositories();
        
        var app = builder.Build();
        
        ServiceHelper.Initialize(app.Services);
        
        return app;
    }
}