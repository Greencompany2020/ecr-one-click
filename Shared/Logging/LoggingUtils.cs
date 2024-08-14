using FluentResults;

namespace EcrOneClick.Logging;

public static class LoggingUtils
{
    public static string FetchErrorsText(List<IError> errors)
    {
        var errorMessages = new List<string>();
        
        errors.ForEach(e => errorMessages.Add(e.Message));
        return string.Join(" | ", errorMessages);
    }
}