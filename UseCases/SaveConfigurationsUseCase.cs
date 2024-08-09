using EcrOneClick.Domain.Entities;
using EcrOneClick.Domain.Repositories;
using EcrOneClick.UseCases.Abstract;
using EcrOneClick.UseCases.Request;

namespace EcrOneClick.UseCases;

public class SaveConfigurationsUseCase(IConfigurationsRepository repository)
    : ISaveConfigurationUseCase
{
    public void Execute(SaveConfigurationValuesRequest request)
    {
        var configurations = new Configurations()
        {
            Id = request.Id,
            Store = request.Store,
            CashRegister = request.CashRegister,
            DockerUser = request.DockerUser,
            DockerPass = request.DockerPass,
            DockerToken = request.DockerToken,
            DopplerToken = request.DopplerToken
        };
        
        repository.SaveConfigurations(configurations);
    }

    /// <summary>
    /// To implement.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task ExecuteAsync(SaveConfigurationValuesRequest request)
    {
        throw new NotImplementedException();
    }
}