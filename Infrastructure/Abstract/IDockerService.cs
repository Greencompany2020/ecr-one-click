using EcrOneClick.Infrastructure.Data;
using FluentResults;

namespace EcrOneClick.Infrastructure.Abstract;

public interface IDockerService
{
    Task<Result<List<DockerImage>>> GetImages();
    
    /// <summary>
    /// Verifica si el host esta en modo swarm.
    /// </summary>
    /// <returns>Cuando no esta en modo swarm retornamos Result.Fail</returns>
    Task<Result<bool>> IsInSwarmMode();
}