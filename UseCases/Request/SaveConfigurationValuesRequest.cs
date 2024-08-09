namespace EcrOneClick.UseCases.Request;

/// <summary>
/// Datos para el caso de uso de
/// almacenamiento de configuracion
/// </summary>
/// <param name="Store"></param>
/// <param name="CashRegister"></param>
/// <param name="DockerUser"></param>
/// <param name="DockerPass"></param>
/// <param name="DockerToken"></param>
/// <param name="DopplerToken"></param>
public record SaveConfigurationValuesRequest(
    int Id,
    string Store,
    string CashRegister,
    string DockerUser,
    string DockerPass,
    string DockerToken,
    string DopplerToken
);