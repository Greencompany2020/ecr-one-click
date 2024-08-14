using EcrOneClick.UseCases.Request;
using FluentResults;

namespace EcrOneClick.UseCases.Abstract;

public interface ISetSwarmMode
{
    Result Execute(SetSwarmModeRequest request);
    
    Task<Result> ExecuteAsync(SetSwarmModeRequest request);
}