namespace EcrOneClick.Domain.Entities;

public class Settings
{
    public int Id { get; set;  }
    public string Store { get; set; } = string.Empty;
    public string CashRegister { get; set; } = string.Empty;
    public string DockerUser { get; set; } = string.Empty;
    public string DockerPass { get; set; } = string.Empty;
    public string DockerToken { get; set; } = string.Empty;
    public string DopplerToken { get; set; } = string.Empty;
}