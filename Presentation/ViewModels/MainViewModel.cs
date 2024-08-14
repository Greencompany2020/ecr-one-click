using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Presentation.Abstract;
using EcrOneClick.UseCases.Abstract;
using EcrOneClick.UseCases.Request;
using Microsoft.Extensions.Logging;

namespace EcrOneClick.Presentation.ViewModels;

public class MainViewModel : IBaseViewModel
{
    private readonly IDockerService _dockerService;
    
    private readonly ILogger<MainViewModel> _logger;
    
    private readonly IGetSwarmMode _getSwarmMode;

    private readonly ISetSwarmMode _setSwarmMode;
    
    public MainViewModel(
        IDockerService dockerService,
        ILogger<MainViewModel> logger,
        IGetSwarmMode getSwarmMode,
        ISetSwarmMode setSwarmMode
        )
    {
        _dockerService = dockerService;
        _logger = logger;
        _getSwarmMode = getSwarmMode;
        _setSwarmMode = setSwarmMode;
    }

    public async void BeginSwarmMode()
    {
        var swarmMode = _getSwarmMode.Execute();

        if (swarmMode.IsFailed)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo obtener configuracion de docker", "OK");
            _logger.LogError("[{Model}.{Method}]: {Error}", nameof(MainViewModel), nameof(BeginSwarmMode), string.Join(",", swarmMode.Errors));
            return;            
        }
        
        if (!swarmMode.Value)
        {
            var result = await _dockerService.BeginSwarmMode();

            if (result.IsFailed)
            {
                _setSwarmMode.Execute(new (false));
                
                _logger.LogInformation("[{Model}.{Method}]: Cannot init Docker Swarm Mode. Using default containers mode", nameof(MainViewModel), nameof(BeginSwarmMode));
            }
            else
            { 
                _setSwarmMode.Execute(new (true));
                _logger.LogInformation("[{Model}.{Method}]: Swarm Mode initialized", nameof(MainViewModel), nameof(BeginSwarmMode));
            }
        }
        else
        {
            _logger.LogInformation("[{Model}.{Method}]: Swarm Mode is already running", nameof(MainViewModel), nameof(BeginSwarmMode));
        }
    }
}