using System.Text.Json.Serialization;

namespace EcrOneClick.Infrastructure.Data;

public class DopplerSecretsResponse
{
    [JsonPropertyName("secrets")]
    public IDictionary<string, DopplerSecretsItemResponse> Secrets { get; set; }
    
    [JsonPropertyName("success")]
    public bool Success { get; set; }
}

public class DopplerSecretsItemResponse
{
    [JsonPropertyName("raw")]
    public string Raw { get; set; }
    
    [JsonPropertyName("computed")]
    public string Computed { get; set; }
    
    [JsonPropertyName("note")]
    public string Note { get; set; }
    
    [JsonPropertyName("rawVisibility")]
    public string RawVisibility { get; set; }
    
    [JsonPropertyName("computedVisibility")]
    public string ComputedVisibility { get; set; }

    [JsonPropertyName("rawValueType")]
    public DopplerSecretsItemValueType RawValueType { get; set; }
    
    [JsonPropertyName("computedValueType")]
    public DopplerSecretsItemValueType ComputedValueType { get; set; }
}

public class DopplerSecretsItemValueType
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
}