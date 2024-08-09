using EcrOneClick.Domain.Entities;

namespace EcrOneClick.Domain.Repositories;

public interface IConfigurationsRepository
{
    void SaveConfigurations(Configurations configurations);
    Configurations GetConfiguration();
}