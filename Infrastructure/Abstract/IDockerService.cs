using EcrOneClick.Infrastructure.Data;
using FluentResults;

namespace EcrOneClick.Infrastructure.Abstract;

public interface IDockerService
{
    bool IsInSwarmMode { get; set; }
    
    Task<Result<List<DockerImage>>> GetContainers();
    
    Task<Result<bool>> BeginSwarmMode();
}