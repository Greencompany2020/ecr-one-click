using EcrOneClick.DI;
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
}