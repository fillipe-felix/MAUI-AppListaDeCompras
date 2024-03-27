using System.Globalization;

using AppListaDeCompras.Models;

namespace AppListaDeCompras.Libraries.Converters;

public class TextQuantityOfItensNotCaughtConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        List<Product> products = (List<Product>)value!;

        var notCaughtCount = products.Count(p => !p.HasCaught);

        return notCaughtCount > 0 ? $"Faltam {notCaughtCount} itens" : $"Falta {notCaughtCount} item";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
