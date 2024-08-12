using CommunityToolkit.Mvvm.ComponentModel;
using EcrOneClick.Presentation.Abstract;
using EcrOneClick.Presentation.Models;
using EcrOneClick.UseCases.Abstract;
using EcrOneClick.UseCases.Request;

namespace EcrOneClick.Presentation.ViewModels;

public partial class ConfigurationsViewModel : ObservableObject, IBaseViewModel
{
    [ObservableProperty]
    private ConfigurationsUiModel _config = new();

    [ObservableProperty]
    private bool _hidePassword = true;

    [ObservableProperty]
    private bool _hideDockerToken = true;

    [ObservableProperty]
    private bool _hideDopplerToken = true;

    private readonly ISaveConfigurationUseCase _saveConfiguration;

    private readonly IGetConfigurationsUseCase _getConfigurations;
    
    public ConfigurationsViewModel(
        ISaveConfigurationUseCase saveConfiguration,
        IGetConfigurationsUseCase getConfigurations
        )
    {
        _saveConfiguration = saveConfiguration;
        _getConfigurations = getConfigurations;
    }

    public void TogglePassword()
    {
        HidePassword = !HidePassword;
    }

    public void ToggleDockerToken()
    {
        HideDockerToken = !HideDockerToken;
    }

    public void ToggleDopplerToken()
    {
        HideDopplerToken = !HideDopplerToken;
    }

    public void SaveConfigValues(SaveConfigurationValuesRequest request)
    {
        Config.Store = request.Store;
        Config.CashRegister = request.CashRegister;
        Config.DockerUser = request.DockerUser;
        Config.DockerPass = request.DockerPass;
        Config.DockerToken = request.DockerToken;
        Config.DopplerToken = request.DopplerToken;
        
        _saveConfiguration.Execute(request);
    }

    public void LoadConfigurations()
    {
        var configurations = _getConfigurations.Execute();
        
        Config.Store = configurations.Store;
        Config.CashRegister = configurations.CashRegister;
        Config.DockerUser = configurations.DockerUser;
        Config.DockerPass = configurations.DockerPass;
        Config.DockerToken = configurations.DockerToken;
        Config.DopplerToken = configurations.DopplerToken;
        
    }
}