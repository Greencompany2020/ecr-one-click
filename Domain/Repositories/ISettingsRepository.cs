using EcrOneClick.Domain.Entities;
using FluentResults;

namespace EcrOneClick.Domain.Repositories;

public interface ISettingsRepository
{
    void SaveSettings(Settings settings);
    Settings GetSettings();
    Result SetSwarmMode(bool isInSwarmMode);
    Result<bool> GetSwarmMode();
}