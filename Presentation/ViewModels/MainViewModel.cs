using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Presentation.Abstract;
using Microsoft.Extensions.Logging;

namespace EcrOneClick.Presentation.ViewModels;

public class MainViewModel : IBaseViewModel
{
    private readonly IDockerService _dockerService;

    private readonly ILogger<MainViewModel> _logger;
    
    public MainViewModel(IDockerService dockerService, ILogger<MainViewModel> logger)
    {
        _dockerService = dockerService;
        _logger = logger;
    }

    public async void BeginSwarmMode()
    {
        if (!_dockerService.IsInSwarmMode)
        {
            var result = await _dockerService.BeginSwarmMode();

            if (result.IsFailed)
            {
                _logger.LogInformation("[{Model}.{Method}]: Cannot init Docker Swarm Mode. Using default containers mode", nameof(MainViewModel), nameof(BeginSwarmMode));
            }
            else
            {
                _logger.LogInformation("[{Model}.{Method}]: Swarm Mode Initialized", nameof(MainViewModel), nameof(BeginSwarmMode));
            }
        }
        else
        {
            _logger.LogInformation("[{Model}.{Method}]: Swarm Mode Initialized", nameof(MainViewModel), nameof(BeginSwarmMode));
        }
    }
}