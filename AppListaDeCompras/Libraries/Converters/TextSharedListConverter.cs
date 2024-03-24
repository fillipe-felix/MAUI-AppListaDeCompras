using System.Globalization;

namespace AppListaDeCompras.Libraries.Converters;

public class TextSharedListConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int? usersCount = (int?)value;

        if (!usersCount.HasValue)
        {
            return null;
        }

        return usersCount >= 2 ? "(Lista compartilhada)" : "";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
