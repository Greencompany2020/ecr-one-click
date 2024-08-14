using EcrOneClick.Domain.Repositories;
using EcrOneClick.UseCases.Abstract;
using FluentResults;

namespace EcrOneClick.UseCases;

public class GetSwarmMode : IGetSwarmMode
{
    private readonly ISettingsRepository _repository;

    public GetSwarmMode(ISettingsRepository repository)
    {
        _repository = repository;
    }
    
    public Result<bool> Execute()
    {
        return _repository.GetSwarmMode();
    }

    public Task<Result<bool>> ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}