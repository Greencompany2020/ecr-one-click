using EcrOneClick.Domain.Repositories;
using EcrOneClick.Infrastructure.Database.Models;
using FluentResults;
using Microsoft.Extensions.Logging;
using SQLite;

namespace EcrOneClick.Infrastructure.Database.Repositories;

public class SqliteSettingsRepository : ISettingsRepository
{
    private readonly SQLiteConnection _conn;
    private readonly ILogger<SqliteSettingsRepository> _logger;

    public SqliteSettingsRepository(SQLiteConnection conn, ILogger<SqliteSettingsRepository> logger)
    {
        _conn = conn;
        _conn.CreateTable<SettingsModel>();
        _conn.CreateTable<DockerSettingsModel>();
        _logger = logger;
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
    // TODO: Agregar Result
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

    public Result SetSwarmMode(bool isInSwarmMode)
    {
        try
        {
            var result = _conn.Table<DockerSettingsModel>()
                .FirstOrDefault();

            if (result is null)
            {
                var model = new DockerSettingsModel()
                {
                    IsInSwarmMode = isInSwarmMode ? "Y" : "N"
                };

                _conn.Insert(model);
                
                _logger.LogInformation("[{Repository}.{Method}]: Inserted new docker settings record. IsInSwarmMode: {Value}",
                    nameof(SqliteSettingsRepository),
                    nameof(SetSwarmMode),
                    isInSwarmMode);
            }
            else
            {
                result.IsInSwarmMode = isInSwarmMode ? "Y" : "N";

                _conn.Update(result);
                
                _logger.LogInformation("[{Repository}.{Method}]: Updated docker settings. IsInSwarmMode: {Value}",
                    nameof(SqliteSettingsRepository),
                    nameof(SetSwarmMode),
                    isInSwarmMode);
            }


            return Result.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError("[{Repository}.{Method}]: {Error}", nameof(SqliteSettingsRepository), nameof(SetSwarmMode), e.Message);
            return Result.Fail(e.Message);
        }
    }

    public Result<bool> GetSwarmMode()
    {
        try
        {
            var result = _conn.Table<DockerSettingsModel>().FirstOrDefault();

            if (result is null)
            {
                _logger.LogInformation("[{Repository}.{Method}]: No records for Docker Settings. Returning default value: false", nameof(SqliteSettingsRepository), nameof(GetSwarmMode));
                return Result.Ok(false);
            }

            _logger.LogInformation("[{Repository}.{Method}]: Found Docker Settings records", nameof(SqliteSettingsRepository), nameof(GetSwarmMode));
            var swarmMode = result.IsInSwarmMode == "Y";

            return Result.Ok(swarmMode);
        }
        catch (Exception e)
        {
            _logger.LogError("[{Repository}.{Method}]: {Error}", nameof(SqliteSettingsRepository), nameof(GetSwarmMode), e.Message);
            return Result.Fail(e.Message);
        }
    }
    
}