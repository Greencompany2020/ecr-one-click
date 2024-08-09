using EcrOneClick.Domain.Repositories;
using EcrOneClick.Infrastructure.Database.Models;
using SQLite;

namespace EcrOneClick.Infrastructure.Database.Repositories;

public class SqliteConfigurationsRepository : IConfigurationsRepository
{
    private readonly SQLiteConnection _conn;

    public SqliteConfigurationsRepository(SQLiteConnection conn)
    {
        _conn = conn;
        _conn.CreateTable<ConfigurationsModel>();
    }


    public void SaveConfigurations(Domain.Entities.Configurations configurations)
    {
        var model = new ConfigurationsModel
        {
            Id = configurations.Id,
            Store = configurations.Store,
            CashRegister = configurations.CashRegister,
            DockerUser = configurations.DockerUser,
            DockerPass = configurations.DockerPass,
            DockerToken = configurations.DockerToken,
            DopplerToken = configurations.DockerToken
        };

        _conn.Insert(model);
    }

    public Domain.Entities.Configurations GetConfiguration()
    {
        var result = _conn.Table<ConfigurationsModel>()
            .FirstOrDefault();

        if (result is null)
        {
            return new Domain.Entities.Configurations();
        }

        var configurations = new Domain.Entities.Configurations()
        {
            Id = result.Id,
            Store = result.Store,
            CashRegister = result.CashRegister,
            DockerUser = result.DockerUser,
            DockerPass = result.DockerPass,
            DockerToken = result.DockerToken,
            DopplerToken = result.DopplerToken
        };

        return configurations;
    }
}