using SQLite;

namespace EcrOneClick.Infrastructure.Database.Models;

public class DockerSettingsModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [NotNull, MaxLength(1)]
    public string IsInSwarmMode { get; set; }
}