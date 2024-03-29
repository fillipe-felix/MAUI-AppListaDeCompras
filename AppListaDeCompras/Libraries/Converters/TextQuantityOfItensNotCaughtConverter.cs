using System.Globalization;

using AppListaDeCompras.Models;

namespace AppListaDeCompras.Libraries.Converters;

public class TextQuantityOfItensNotCaughtConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return "Faltam 0 item";
        }
        
        IList<Product> products = (IList<Product>)value!;

        var notCaughtCount = products.Count(p => !p.HasCaught);

        return notCaughtCount > 1 ? $"Faltam {notCaughtCount} itens" : $"Falta {notCaughtCount} item";
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
