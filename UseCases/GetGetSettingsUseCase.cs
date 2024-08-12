using EcrOneClick.Domain.Entities;
using EcrOneClick.Domain.Repositories;
using EcrOneClick.UseCases.Abstract;

namespace EcrOneClick.UseCases;

public class GetGetSettingsUseCase(ISettingsRepository repository) : IGetSettingsUseCase
{
    public Settings Execute()
    {
        return repository.GetSettings();
    }

    public Task<Settings> ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}