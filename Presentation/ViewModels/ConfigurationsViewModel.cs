using CommunityToolkit.Mvvm.ComponentModel;
using EcrOneClick.Presentation.Abstract;
using EcrOneClick.Presentation.Models;
using EcrOneClick.UseCases.Request;

namespace EcrOneClick.Presentation.ViewModels;

public partial class ConfigurationsViewModel : ObservableObject, IBaseViewModel
{
    [ObservableProperty]
    private Configurations _config = new();

    [ObservableProperty]
    private bool _hidePassword = true;

    [ObservableProperty]
    private bool _hideDockerToken = true;

    [ObservableProperty]
    private bool _hideDopplerToken = true;
    
    public ConfigurationsViewModel()
    {
        _config.DockerUser = "";
        _config.DockerPass = "";
        _config.DockerToken = "";
        _config.DopplerToken = "";
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
        Config.DockerUser = request.DockerUser;
        Config.DockerPass = request.DockerPass;
        Config.DockerToken = request.DockerToken;
        Config.DopplerToken = request.DopplerToken;
    }
}