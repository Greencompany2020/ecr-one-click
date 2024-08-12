using EcrOneClick.Domain.Entities;
using EcrOneClick.Domain.Repositories;
using EcrOneClick.UseCases.Abstract;
using EcrOneClick.UseCases.Request;

namespace EcrOneClick.UseCases;

public class SaveSettingsesUseCase(ISettingsRepository repository)
    : ISaveSettingsUseCase
{
    public void Execute(SaveSettingsValuesRequest request)
    {
        var configurations = new Settings()
        {
            Id = request.Id,
            Store = request.Store,
            CashRegister = request.CashRegister,
            DockerUser = request.DockerUser,
            DockerPass = request.DockerPass,
            DockerToken = request.DockerToken,
            DopplerToken = request.DopplerToken
        };
        
        repository.SaveSettings(configurations);
    }

    /// <summary>
    /// To implement.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task ExecuteAsync(SaveSettingsValuesRequest request)
    {
        throw new NotImplementedException();
    }
}