using System.Diagnostics;
using EcrOneClick.DI;
using EcrOneClick.Presentation.ViewModels;
using EcrOneClick.UseCases.Request;
using CommunityToolkit.Maui.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace EcrOneClick.Presentation.Views;

public partial class SettingsPage : ContentPage
{
    private readonly IValidator<SaveSettingsValuesRequest> _validator;

    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<SettingsViewModel>();
        _validator = ServiceHelper.GetService<IValidator<SaveSettingsValuesRequest>>();
    }
    
    private SettingsViewModel GetViewModel()
    {
        var viewModel = (SettingsViewModel)BindingContext;
        return viewModel;
    }
    
    private void ResetErrorsText()
    {
        StoreErrorText.Text = "";
        StoreErrorText.IsVisible = false;

        CashRegisterErrorText.Text = "";
        CashRegisterErrorText.IsVisible = false;

        DockerUserErrorText.Text = "";
        DockerUserErrorText.IsVisible = false;

        DockerPassErrorText.Text = "";
        DockerUserErrorText.IsVisible = false;

        DockerTokenErrorText.Text = "";
        DockerTokenErrorText.IsVisible = false;

        DopplerTokenErrorText.Text = "";
        DopplerTokenErrorText.IsVisible = false;
    }

    private void SetErrors(List<ValidationFailure> errors) 
    {
        foreach (var error in errors)
        {
            switch (error.PropertyName)
            {
                case "CashRegister":
                    CashRegisterErrorText.Text = error.ErrorMessage;
                    CashRegisterErrorText.IsVisible = true;
                    break;
                case "Store":
                    StoreErrorText.Text = error.ErrorMessage;
                    StoreErrorText.IsVisible = true;
                    break;
                case "DockerUser":
                    DockerUserErrorText.Text = error.ErrorMessage;
                    DockerUserErrorText.IsVisible = true;
                    break;
                case "DockerPass":
                    DockerPassErrorText.Text = error.ErrorMessage;
                    DockerPassErrorText.IsVisible = true;
                    break;
                case "DockerToken":
                    DockerTokenErrorText.Text = error.ErrorMessage;
                    DockerTokenErrorText.IsVisible = true;
                    break;
                case "DopplerToken":
                    DopplerTokenErrorText.Text = error.ErrorMessage;
                    DopplerTokenErrorText.IsVisible = true;
                    break;
            }
        }
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


        var id = 0;

        if (!string.IsNullOrEmpty(SettingsId.Text))
        {
            id = Convert.ToInt32(SettingsId.Text);
        }
        
        SaveSettingsValuesRequest request = new (id, store, cashRegister, dockerUser, dockerPass, dockerToken, dopplerToken);

        var result = await _validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            ResetErrorsText();
            
            SetErrors(result.Errors);
        }
        else
        {
            ResetErrorsText();

            var viewModel = GetViewModel();
        
            viewModel.SaveSettingsValues(request);
            
            await DisplayAlert("Configuraciones", "Se han guardado los valores", "OK");
            
            viewModel.LoadSettings();
        }
    }

    protected override void OnAppearing()
    {
        GetViewModel().LoadSettings();
    }
}