using EcrOneClick.Infrastructure.Data;
using FluentResults;

namespace EcrOneClick.Infrastructure.Abstract;

public interface IDopplerService
{
    Task<Result<DopplerSecretsResponse?>> GetDopplerSecretsAsync(string projectName, string environment,
        string accessToken);
}