using CommunityToolkit.Mvvm.ComponentModel;

namespace EcrOneClick.Presentation.Models;

public partial class ConfigurationsUiModel : ObservableObject
{
    public int Id { get; set;  }

    [ObservableProperty]
    private string _store;
    
    [ObservableProperty]
    private string _cashRegister;
    
    [ObservableProperty]
    private string _dockerUser;
    
    [ObservableProperty]
    private string _dockerPass;
    
    [ObservableProperty]
    private string _dockerToken;
    
    [ObservableProperty]
    private string _dopplerToken;
}