using EcrOneClick.Domain.Repositories;
using EcrOneClick.Infrastructure.Database.Models;
using SQLite;

namespace EcrOneClick.Infrastructure.Database.Repositories;

public class SqliteSettingsRepository : ISettingsRepository
{
    private readonly SQLiteConnection _conn;

    public SqliteSettingsRepository(SQLiteConnection conn)
    {
        _conn = conn;
        _conn.CreateTable<SettingsModel>();
    }


    public void SaveSettings(Domain.Entities.Settings settings)
    {
        var existingSettings = _conn.Table<SettingsModel>()
            .FirstOrDefault(settingsModel => settingsModel.Id == settings.Id);

        var model = new SettingsModel
        {
            Id = settings.Id,
            Store = settings.Store,
            CashRegister = settings.CashRegister,
            DockerUser = settings.DockerUser,
            DockerPass = settings.DockerPass,
            DockerToken = settings.DockerToken,
            DopplerToken = settings.DockerToken
        };
        
        if (existingSettings is null)
        {

            _conn.Insert(model);            
        }
        else
        {
            _conn.Update(model);
        }
    }

    public Domain.Entities.Settings GetSettings()
    {
        var result = _conn.Table<SettingsModel>()
            .FirstOrDefault();

        if (result is null)
        {
            return new Domain.Entities.Settings();
        }

        var settings = new Domain.Entities.Settings()
        {
            Id = result.Id,
            Store = result.Store,
            CashRegister = result.CashRegister,
            DockerUser = result.DockerUser,
            DockerPass = result.DockerPass,
            DockerToken = result.DockerToken,
            DopplerToken = result.DopplerToken
        };

        return settings;
    }
}