using EcrOneClick.Domain.Entities;
using EcrOneClick.Domain.Repositories;
using EcrOneClick.UseCases.Abstract;

namespace EcrOneClick.UseCases;

public class GetConfigurationsUseCase(IConfigurationsRepository repository) : IGetConfigurationsUseCase
{
    public Configurations Execute()
    {
        return repository.GetConfiguration();
    }

    public Task<Configurations> ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}