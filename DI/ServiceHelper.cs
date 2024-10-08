using EcrOneClick.Domain.Repositories;
using EcrOneClick.Infrastructure;
using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Infrastructure.Database.Repositories;
using EcrOneClick.Presentation.Abstract;
using EcrOneClick.Presentation.ViewModels.Validators;
using EcrOneClick.UseCases;
using EcrOneClick.UseCases.Abstract;
using EcrOneClick.UseCases.Request;
using FluentValidation;

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

    public static MauiAppBuilder LoadValidators(this MauiAppBuilder builder)
    {
        builder.Services
            .AddSingleton<IValidator<SaveSettingsValuesRequest>, SaveSettingsValuesRequestValidator>();

        return builder;
    }

    public static MauiAppBuilder LoadRepositories(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ISettingsRepository, SqliteSettingsRepository>();

        return builder;
    }

    public static MauiAppBuilder LoadUseCases(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ISaveSettingsUseCase, SaveSettingsUseCase>();
        builder.Services.AddSingleton<IGetSettingsUseCase, GetSettingsUseCase>();

        return builder;
    }
}