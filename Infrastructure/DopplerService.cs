using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Infrastructure.Data;
using FluentResults;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EcrOneClick.Infrastructure;

public class DopplerService(ILogger<DopplerService> logger) : IDopplerService
{
    private HttpClient? _httpClient;

    public async Task<Result<DopplerSecretsResponse?>> GetDopplerSecretsAsync(string projectName, string environment,
        string accessToken)
    {
        try
        {
            _httpClient ??= new HttpClient
            {
                DefaultRequestHeaders =
                {
                    { "Accept", "application/json" },
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };
            
            var dopplerResponse = await _httpClient.GetAsync(new Uri($"https://api.doppler.com/v3/configs/config/secrets?project={projectName}&config={environment}"));

            if (!dopplerResponse.IsSuccessStatusCode)
            {
                var nonSuccessContent = await dopplerResponse.Content.ReadAsStringAsync();
                var nonSuccessResponse = JsonConvert.DeserializeObject<DopplerNotSuccessResponse>(nonSuccessContent);

                var errorMessages = string.Join(" | ", nonSuccessResponse?.Messages);
                
                logger.LogWarning("[{Service}.{Method}]: Cannot fetch secrets. {Error}", nameof(DopplerService), nameof(GetDopplerSecretsAsync), errorMessages);
                
                return Result.Fail(errorMessages);
            }
            
            var content = await dopplerResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<DopplerSecretsResponse>(content);
                
            logger.LogWarning("[{Service}.{Method}]: Fetched doppler secrets for ${ProjectName}", nameof(DopplerService), nameof(GetDopplerSecretsAsync), projectName);
            
            return Result.Ok(response);

        }
        catch (Exception e)
        {
            logger.LogError(e, "[{Service}.{Method}]: {Error}", nameof(DopplerService), nameof(GetDopplerSecretsAsync), e.Message);
            return Result.Fail(e.Message);
        }
    }
}