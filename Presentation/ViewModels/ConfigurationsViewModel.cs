using CommunityToolkit.Mvvm.ComponentModel;
using EcrOneClick.Presentation.Abstract;
using EcrOneClick.Presentation.Models;

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
        _config.DockerUser = "Dti";
        _config.DockerPass = "Sist3mas";
        _config.DockerToken = "Accesstoken";
        _config.DopplerToken = "Token";
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
}