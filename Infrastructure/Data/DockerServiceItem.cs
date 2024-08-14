namespace EcrOneClick.Infrastructure.Data;

/// <summary>
/// Representa un servicio o contenedor. Estos terminos
/// se usan de manera equivalente, ya que es indiferente si
/// se definen servicios (en modo Swarm) o contenedores.
/// </summary>
public class DockerServiceItem
{
    public string Name { get; set; }
    public string Status { get; set; }
}