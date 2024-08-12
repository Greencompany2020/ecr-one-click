using CommunityToolkit.Mvvm.ComponentModel;
using EcrOneClick.Presentation.Abstract;
using EcrOneClick.Presentation.Models;
using EcrOneClick.UseCases.Abstract;
using EcrOneClick.UseCases.Request;

namespace EcrOneClick.Presentation.ViewModels;

public partial class SettingsViewModel : ObservableObject, IBaseViewModel
{
    [ObservableProperty]
    private SettingsUiModel _settings = new();

    [ObservableProperty]
    private bool _hidePassword = true;

    [ObservableProperty]
    private bool _hideDockerToken = true;

    [ObservableProperty]
    private bool _hideDopplerToken = true;

    private readonly ISaveSettingsUseCase _saveSettings;

    private readonly IGetSettingsUseCase _getSettings;
    
    public SettingsViewModel(
        ISaveSettingsUseCase saveSettings,
        IGetSettingsUseCase getSettings
        )
    {
        _saveSettings = saveSettings;
        _getSettings = getSettings;
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

    public void SaveSettingsValues(SaveSettingsValuesRequest request)
    {
        Settings.Store = request.Store;
        Settings.CashRegister = request.CashRegister;
        Settings.DockerUser = request.DockerUser;
        Settings.DockerPass = request.DockerPass;
        Settings.DockerToken = request.DockerToken;
        Settings.DopplerToken = request.DopplerToken;
        
        _saveSettings.Execute(request);
    }

    public void LoadSettings()
    {
        var settings = _getSettings.Execute();
        
        Settings.Store = settings.Store;
        Settings.CashRegister = settings.CashRegister;
        Settings.DockerUser = settings.DockerUser;
        Settings.DockerPass = settings.DockerPass;
        Settings.DockerToken = settings.DockerToken;
        Settings.DopplerToken = settings.DopplerToken;
        
    }
}