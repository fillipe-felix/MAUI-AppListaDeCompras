using System.Globalization;

namespace AppListaDeCompras.Libraries.Converters;

public class TextQuantityItemOfListConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var productsCount = (int)value!;

        return productsCount > 0 ? " itens" : " item";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
