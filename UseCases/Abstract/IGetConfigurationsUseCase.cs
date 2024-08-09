using EcrOneClick.Domain.Entities;

namespace EcrOneClick.UseCases.Abstract;

public interface IGetConfigurationsUseCase
{
    Configurations Execute();
    Task<Configurations> ExecuteAsync();
}