using EcrOneClick.DI;
using EcrOneClick.Presentation.ViewModels;

namespace EcrOneClick.Presentation.Views;

public partial class ConfigurationsPage : ContentPage
{
    public ConfigurationsPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<ConfigurationsViewModel>();
    }

    private void OnDockerPasswordHideButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = (ConfigurationsViewModel)BindingContext;
        
        viewModel.TogglePassword();
        
        DockerPasswordHideButton.Source = viewModel.HidePassword ? "closed_eye.png" : "open_eye.png";
    }

    private void OnDockerTokenHidButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = (ConfigurationsViewModel)BindingContext;
        
        viewModel.ToggleDockerToken();
        
        DockerPasswordHideButton.Source = viewModel.HideDockerToken? "closed_eye.png" : "open_eye.png";
    }

    private void OnDopplerTokenHideButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = (ConfigurationsViewModel)BindingContext;
        
        viewModel.ToggleDopplerToken();
            
        DopplerTokenHideButton.Source = viewModel.HideDopplerToken ? "closed_eye.png" : "open_eye.png";
    }
}