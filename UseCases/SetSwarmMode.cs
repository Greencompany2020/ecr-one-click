using EcrOneClick.Domain.Repositories;
using EcrOneClick.UseCases.Abstract;
using EcrOneClick.UseCases.Request;
using FluentResults;

namespace EcrOneClick.UseCases;

public class SetSwarmMode : ISetSwarmMode
{
    private readonly ISettingsRepository _repository;
    
    public SetSwarmMode(ISettingsRepository repository)
    {
        _repository = repository;
    }
    
    public Result Execute(SetSwarmModeRequest request)
    {
        return _repository.SetSwarmMode(request.IsInSwarmMode);
    }

    public Task<Result> ExecuteAsync(SetSwarmModeRequest request)
    {
        throw new NotImplementedException();
    }
}