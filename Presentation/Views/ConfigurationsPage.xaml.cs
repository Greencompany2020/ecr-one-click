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
}