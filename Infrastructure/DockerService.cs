using Docker.DotNet;
using Docker.DotNet.Models;
using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Infrastructure.Data;
using FluentResults;

namespace EcrOneClick.Infrastructure;

public class DockerService : IDockerService
{
    private readonly DockerClient _dockerClient;
    
    public DockerService()
    {
        _dockerClient = new DockerClientConfiguration().CreateClient();
    }   
    
    public async Task<Result<List<DockerImage>>> GetImages()
    {
        try
        {
            var images = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters()
            {
                All = true
            });

            if (images is null) return Result.Ok(new List<DockerImage>());

            List<DockerImage> imageList = [];

            foreach (var image in images)
            {
                imageList.Add(new DockerImage()
                {
                    Name = string.Join(",", image.Names),
                    Status = image.Status
                });
            }

            return Result.Ok(imageList);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }

    public async Task<Result<bool>> IsInSwarmMode()
    {
        try
        {
            var result = await _dockerClient.Swarm.InspectSwarmAsync();

            if (result is not null) Result.Ok(true);
            
            // En realidad no verificamos si es true o false en donde se llama.
            // Cuando no esta en modo swarm arroja una excepcion y con ello
            // validamos el estatus del nodo.
            return Result.Ok(false);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}