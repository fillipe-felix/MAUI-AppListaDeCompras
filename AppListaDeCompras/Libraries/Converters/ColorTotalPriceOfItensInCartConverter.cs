using System.Globalization;

namespace AppListaDeCompras.Libraries.Converters;

public class ColorTotalPriceOfItensInCartConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var hasCaught = (bool)value!;
        
        return hasCaught ? Colors.Black : Colors.Gray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
