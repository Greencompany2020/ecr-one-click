using EcrOneClick.Infrastructure;
using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Presentation.Abstract;

namespace EcrOneClick.DI;

public static class ServiceHelper
{
    private static IServiceProvider Services { get; set; }

    public static void Initialize(IServiceProvider serviceProvider) => Services = serviceProvider;

    public static T? GetService<T>() => Services.GetService<T>();

    public static MauiAppBuilder LoadViewModels(this MauiAppBuilder builder)
    {
        builder.Services.Scan(scan => scan.FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo<IBaseViewModel>())
            .AsSelf()
            .WithSingletonLifetime()
        );

        return builder;
    }

    public static MauiAppBuilder LoadServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IDockerService, DockerService>();

        return builder;
    }
}