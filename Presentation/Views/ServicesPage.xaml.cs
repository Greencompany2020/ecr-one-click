using EcrOneClick.DI;
using EcrOneClick.Logging;
using EcrOneClick.Presentation.ViewModels;

namespace EcrOneClick.Presentation.Views;

public partial class ServicesPage : ContentPage
{
    public ServicesPage()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<ServicesViewModel>();
    }
    
    protected override void OnAppearing()
    {
        var viewModel = (ServicesViewModel)BindingContext;
        
        viewModel.LoadServices();
    }

    private async void OnInitServicesButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = (ServicesViewModel)BindingContext;
        
        var initServicesResult = await viewModel.InitServices();

        if (initServicesResult.IsSuccess)
        {
            await DisplayAlert("Success", initServicesResult.Value, "OK");
            viewModel.LoadServices();
        }
        else
        {
            await DisplayAlert("Error", LoggingUtils.FetchErrorsText(initServicesResult.Errors), "OK");
        }
    }

    private async void OnFetchEnvButtonClicked(object? sender, EventArgs e)
    {
        var viewModel = (ServicesViewModel)BindingContext;

        var secrets = await viewModel.FetchSecrets();

        if (secrets.IsFailed)
        {
            await DisplayAlert("Error", LoggingUtils.FetchErrorsText(secrets.Errors), "OK");
            return;
        }

        if (secrets.Value is null)
        {
            await DisplayAlert("Error", LoggingUtils.FetchErrorsText(secrets.Errors), "OK");
            return;
        }

        await DisplayAlert("Secrets", secrets.Value.ToString(), "OK");
    }
}