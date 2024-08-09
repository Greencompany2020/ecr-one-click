namespace EcrOneClick.Domain.Entities;

public class Configurations
{
    public int Id { get; set;  }
    public string Store { get; set; }
    public string CashRegister { get; set; }
    public string DockerUser { get; set; }
    public string DockerPass { get; set; }
    public string DockerToken { get; set; }
    public string DopplerToken { get; set; }
}