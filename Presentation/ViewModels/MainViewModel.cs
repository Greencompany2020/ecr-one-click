using System.Diagnostics;
using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Presentation.Abstract;

namespace EcrOneClick.Presentation.ViewModels;

public class MainViewModel : IBaseViewModel
{
    private readonly IDockerService _dockerService;
    
    public MainViewModel(IDockerService dockerService)
    {
        _dockerService = dockerService;
    }

    public async void BeginSwarmMode()
    {
        if (!_dockerService.IsInSwarmMode)
        {
            var result = await _dockerService.BeginSwarmMode();

            if (result.IsFailed)
            {
                Debug.WriteLine(result);
                await Shell.Current.DisplayAlert("Swarm Mode", "No se pudo iniciar el modo Swarm. Utilizando contendores.", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Swarm Mode", "Modo Swarm Iniciado", "OK");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Swarm Mode", "Modo Swarm Iniciado", "OK");
        }
    }
}