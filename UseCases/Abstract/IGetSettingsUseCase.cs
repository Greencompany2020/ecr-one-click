using EcrOneClick.Domain.Entities;

namespace EcrOneClick.UseCases.Abstract;

public interface IGetSettingsUseCase
{
    Settings Execute();
    Task<Settings> ExecuteAsync();
}