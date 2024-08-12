using SQLite;

namespace EcrOneClick.Infrastructure.Database.Models;

public class SettingsModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [NotNull, MaxLength(3)]
    public string Store { get; set; }
    [NotNull, MaxLength(15)]
    public string CashRegister { get; set; }
    [NotNull]
    public string DockerUser { get; set; }
    [NotNull]
    public string DockerPass { get; set; }
    [NotNull]
    public string DockerToken { get; set; }
    [NotNull]
    public string DopplerToken { get; set; }
}