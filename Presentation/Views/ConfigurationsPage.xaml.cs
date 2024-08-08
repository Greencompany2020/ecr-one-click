using EcrOneClick.DI;
using EcrOneClick.Presentation.ViewModels;
using EcrOneClick.UseCases.Request;

namespace EcrOneClick.Presentation.Views;

public partial class ConfigurationsPage : ContentPage
{
    public ConfigurationsPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<ConfigurationsViewModel>();
    }

    private ConfigurationsViewModel GetViewModel()
    {
        var viewModel = (ConfigurationsViewModel)BindingContext;
        return viewModel;
    }
    
    private void OnDockerPasswordHideButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = GetViewModel();
        
        viewModel.TogglePassword();
        
        DockerPasswordHideButton.Source = viewModel.HidePassword ? "closed_eye.png" : "open_eye.png";
    }

    private void OnDockerTokenHidButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = GetViewModel();
        
        viewModel.ToggleDockerToken();
        
        DockerPasswordHideButton.Source = viewModel.HideDockerToken? "closed_eye.png" : "open_eye.png";
    }

    private void OnDopplerTokenHideButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = GetViewModel();
        
        viewModel.ToggleDopplerToken();
            
        DopplerTokenHideButton.Source = viewModel.HideDopplerToken ? "closed_eye.png" : "open_eye.png";
    }

    private void OnSaveButtonClicked(object? sender, EventArgs e)
    {
        var store = StoreEntry.Text;
        var cashRegister = CashRegisterEntry.Text;
        var dockerUser = DockerUserEntry.Text;
        var dockerPass = DockerPasswordEntry.Text;
        var dockerToken = DockerTokenEntry.Text;
        var dopplerToken = DopplerTokenEntry.Text;
        
        var request = new SaveConfigurationValuesRequest(
            store,
            cashRegister,
            dockerUser,
            dockerPass,
            dockerToken,
            dopplerToken
        );
        
        var viewModel = GetViewModel();
        
        viewModel.SaveConfigValues(request);
    }
}