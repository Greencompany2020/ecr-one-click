using Docker.DotNet;
using Docker.DotNet.Models;
using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Infrastructure.Data;
using FluentResults;
using Microsoft.Extensions.Logging;
using ServiceStatus = EcrOneClick.Presentation.Values.ServiceStatus;

namespace EcrOneClick.Infrastructure;

public class DockerService : IDockerService
{
    private readonly DockerClient _dockerClient;
    private readonly ILogger<DockerService> _logger;
    
    public DockerService(ILogger<DockerService> logger)
    {
        _dockerClient = new DockerClientConfiguration().CreateClient();
        _logger = logger;
    }

    public async Task<Result<List<DockerServiceItem>>> GetContainers()
    {
        try
        {
            var containers = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters()
            {
                All = true
            });

            if (containers is null)
            {
                _logger.LogWarning("[{Service}.{Method}]: Returned containers list is NULL", nameof(DockerService), nameof(GetContainers));
                return Result.Ok(new List<DockerServiceItem>());
            }

            List<DockerServiceItem> imageList = [];

            foreach (var container in containers)
            {
                imageList.Add(new DockerServiceItem()
                {
                    Name = string.Join(",", container.Names).Replace("/", ""),
                    Status = container.State == "running" ? ServiceStatus.Active : ServiceStatus.Inactive
                });
            }

            _logger.LogInformation("[{Service}.{Method}]: Returned {Records} containers", nameof(DockerService), nameof(GetContainers), imageList.Count);
            
            return Result.Ok(imageList);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[{Service}.{Method}]: {Error}", nameof(DockerService), nameof(GetContainers), e.Message);
            return Result.Fail(e.Message);
        }
    }

    public async Task<Result<List<DockerServiceItem>>> GetServices()
    {
        try
        {
            var servicesResult = await _dockerClient.Swarm.ListServicesAsync(new ServicesListParameters());

            if (servicesResult is null)
            {
                _logger.LogWarning("[{Service}.{Method}]: Returned service list is NULL", nameof(DockerService), nameof(GetServices));
                return Result.Ok(new List<DockerServiceItem>());
            }

            List<DockerServiceItem> serviceList = [];

            foreach (var service in servicesResult)
            {
                serviceList.Add(new DockerServiceItem
                {
                    Name = service.Spec.Name,
                    Status = service.Spec.Mode.Replicated.Replicas >= 1 ? ServiceStatus.Active : ServiceStatus.Inactive
                });
            }

            _logger.LogInformation("[{Service}.{Method}]: Returned {Records} services", nameof(DockerService), nameof(GetServices), serviceList.Count);
            return Result.Ok(serviceList);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[{Service}.{Method}]: {Error}", nameof(DockerService), nameof(GetServices), e.Message);
            return Result.Fail(e.Message);
        }
    }

    public async Task<Result<bool>> BeginSwarmMode()
    {
        try
        {
            var result = await _dockerClient.Swarm.InitSwarmAsync(new SwarmInitParameters
            {
                ListenAddr = "0.0.0.0"
            });
            // Para testing de uso de contenedores
            // var result = await _dockerClient.Swarm.InitSwarmAsync(new SwarmInitParameters());

            // En realidad no verificamos si es true o false en donde se llama.
            // Cuando no esta en modo swarm arroja una excepcion y con ello
            // validamos el estatus del nodo.
            if (result is null) return Result.Ok(false);
            
            return Result.Ok(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[{Service}.{Method}]: {Error}", nameof(DockerService), nameof(BeginSwarmMode), e.Message);
            return Result.Fail(e.Message);
        }
    }
}