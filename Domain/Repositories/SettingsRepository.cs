using EcrOneClick.Domain.Entities;

namespace EcrOneClick.Domain.Repositories;

public interface ISettingsRepository
{
    void SaveSettings(Settings settings);
    Settings GetSettings();
}