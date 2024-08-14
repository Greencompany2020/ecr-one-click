using EcrOneClick.Infrastructure.Data;
using FluentResults;

namespace EcrOneClick.Infrastructure.Abstract;

public interface IDockerService
{
    Task<Result<List<DockerServiceItem>>> GetContainers();
    Task<Result<List<DockerServiceItem>>> GetServices();
    Task<Result<bool>> BeginSwarmMode();
}