using System.Text.Json.Serialization;

namespace EcrOneClick.Infrastructure.Data;

/// <summary>
/// Respuesta de la API de Doppler cuando ocurre un error (estados 4xx y 5xx).
/// </summary>
public class DopplerNotSuccessResponse
{
    [JsonPropertyName("messages")]
    public List<string> Messages { get; set; }
    
    [JsonPropertyName("success")]
    public bool Success { get; set; }
}