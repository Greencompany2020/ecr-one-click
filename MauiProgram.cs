using EcrOneClick.DI;
using EcrOneClick.Infrastructure;
using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Presentation.ViewModels;
using Microsoft.Extensions.Logging;

namespace EcrOneClick;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
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

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<IDockerService, DockerService>();
        
        var app = builder.Build();
        
        ServiceHelper.Initialize(app.Services);
        
        return app;
    }
}