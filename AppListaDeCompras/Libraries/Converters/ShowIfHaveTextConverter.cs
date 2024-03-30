using System.Globalization;

namespace AppListaDeCompras.Libraries.Converters;

public class ShowIfHaveTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string str = (string)value;
        if (str == null || str.Length == 0)
            return false;

        return true;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
