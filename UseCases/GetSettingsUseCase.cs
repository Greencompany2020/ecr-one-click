using EcrOneClick.Domain.Entities;
using EcrOneClick.Domain.Repositories;
using EcrOneClick.Shared.Encryption.Abstract;
using EcrOneClick.UseCases.Abstract;
using FluentResults;

namespace EcrOneClick.UseCases;

public class GetSettingsUseCase(ISettingsRepository repository, IEncryptionService encryptionService) : IGetSettingsUseCase
{
    public Settings Execute()
    {
        var settings = repository.GetSettings();

        var decryptedResults = Result.Merge(
            encryptionService.DecryptString("", settings.DockerUser),
            encryptionService.DecryptString("", settings.DockerPass),
            encryptionService.DecryptString("", settings.DockerToken),
            encryptionService.DecryptString("", settings.DopplerToken)
            );

        if (decryptedResults.IsFailed)
        {
            return settings;
        }

        var result = decryptedResults.Value.ToArray();

        var decryptedSettings = new Settings
        {
            Id = settings.Id,
            Store = settings.Store,
            CashRegister = settings.CashRegister,
            DockerUser = result[0],
            DockerPass = result[1],
            DockerToken = result[2],
            DopplerToken = result[3]
        };

        return decryptedSettings;
    }

    public Task<Settings> ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}