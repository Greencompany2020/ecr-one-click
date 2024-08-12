using EcrOneClick.UseCases.Request;

namespace EcrOneClick.UseCases.Abstract;

public interface ISaveSettingsUseCase
{
    void Execute(SaveSettingsValuesRequest request);
    Task ExecuteAsync(SaveSettingsValuesRequest request);
}