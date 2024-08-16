using EcrOneClick.Domain.Entities;
using EcrOneClick.Domain.Repositories;
using EcrOneClick.Shared.Encryption.Abstract;
using EcrOneClick.UseCases.Abstract;
using EcrOneClick.UseCases.Request;
using FluentResults;

namespace EcrOneClick.UseCases;

// B1191CDDDF5F1827E677716E99AC3 - SecretKey

public class SaveSettingsUseCase(ISettingsRepository repository, IEncryptionService encryptionService)
    : ISaveSettingsUseCase
{
    public void Execute(SaveSettingsValuesRequest request)
    {
        var encryptedResult = Result.Merge(
            encryptionService.EncryptString("", request.DockerUser),
            encryptionService.EncryptString("", request.DockerPass),
            encryptionService.EncryptString("" ,request.DockerToken),
            encryptionService.EncryptString("", request.DopplerToken)
            );

        if (encryptedResult.IsFailed) return;

        var results = encryptedResult.Value.ToArray();
        
        var configurations = new Settings()
        {
            Id = request.Id,
            Store = request.Store,
            CashRegister = request.CashRegister,
            DockerUser = results[0],
            DockerPass = results[1],
            DockerToken = results[2],
            DopplerToken = results[3]
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