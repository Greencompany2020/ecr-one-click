using EcrOneClick.Presentation.Views;

namespace EcrOneClick;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}