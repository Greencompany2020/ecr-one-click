using EcrOneClick.DI;
using EcrOneClick.Presentation.ViewModels;

namespace EcrOneClick.Presentation.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<MainViewModel>();
    }

    protected override void OnAppearing()
    {
        var viewModel = (MainViewModel)BindingContext;
        viewModel.LoadImages();
    }

    private async void OnSettingsButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SettingsPage");
    }

    private async void OnServicesButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ServicesPage");
    }
}