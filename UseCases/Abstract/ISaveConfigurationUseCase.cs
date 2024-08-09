using EcrOneClick.UseCases.Request;

namespace EcrOneClick.UseCases.Abstract;

public interface ISaveConfigurationUseCase
{
    void Execute(SaveConfigurationValuesRequest request);
    Task ExecuteAsync(SaveConfigurationValuesRequest request);
}