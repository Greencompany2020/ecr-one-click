using CommunityToolkit.Mvvm.ComponentModel;

namespace EcrOneClick.Presentation.Models;

public partial class ConfigurationsUIModel : ObservableObject
{
    public int Id { get; set;  }

    [ObservableProperty]
    private string _store;
    
    [ObservableProperty]
    private string CashRegister;
    
    [ObservableProperty]
    private string DockerUser;
    
    [ObservableProperty]
    private string DockerPass;
    
    [ObservableProperty]
    private string DockerToken;
    
    [ObservableProperty]
    private string DopplerToken;
}