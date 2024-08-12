using System.Globalization;
using EcrOneClick.Presentation.Values;

namespace EcrOneClick.Presentation.Views.Converters;

public class StatusToTextColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var status = (string)value;

        return status == ServiceStatus.Active ? Colors.Green : Colors.Red;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}