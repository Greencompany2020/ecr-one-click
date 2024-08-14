using FluentResults;

namespace EcrOneClick.UseCases.Abstract;

public interface IGetSwarmMode
{
    Result<bool> Execute();
    Task<Result<bool>> ExecuteAsync();
}