using EcrOneClick.DI;
using EcrOneClick.Presentation.ViewModels;
using EcrOneClick.UseCases.Request;
using CommunityToolkit.Maui.Extensions;
using FluentValidation;

namespace EcrOneClick.Presentation.Views;

public partial class ConfigurationsPage : ContentPage
{
    private readonly IValidator<SaveConfigurationValuesRequest> _validator;
    
    public ConfigurationsPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<ConfigurationsViewModel>();
        _validator = ServiceHelper.GetService<IValidator<SaveConfigurationValuesRequest>>() ?? throw new InvalidOperationException();
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
        
        DockerTokenHideButton.Source = viewModel.HideDockerToken? "closed_eye.png" : "open_eye.png";
    }

    private void OnDopplerTokenHideButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = GetViewModel();
        
        viewModel.ToggleDopplerToken();
            
        DopplerTokenHideButton.Source = viewModel.HideDopplerToken ? "closed_eye.png" : "open_eye.png";
    }

    private async void OnSaveButtonClicked(object? sender, EventArgs e)
    {
        await SaveBtn.BackgroundColorTo(Color.FromRgba("#349CDB"));
        await SaveBtn.BackgroundColorTo(Color.FromRgba("#243C78"));
        
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

        var result = await _validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            await DisplayAlert("Error", result.ToString(), "OK");
        }
        else
        {
            var viewModel = GetViewModel();
        
            viewModel.SaveConfigValues(request);

            await DisplayAlert("Configuraciones", "Se han guardado los valores", "OK");            
        }
    }
}