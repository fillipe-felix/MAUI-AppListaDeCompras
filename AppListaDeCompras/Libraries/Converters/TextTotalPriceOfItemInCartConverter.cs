using System.Globalization;

using AppListaDeCompras.Models;

namespace AppListaDeCompras.Libraries.Converters;

public class TextTotalPriceOfItemInCartConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var product = value as Product;

        if (product is null)
        {
            return "R$ 0,00";
        }

        if (product.HasCaught)
        {
            return (product.Quantity * product.Price).ToString("C");
        }
        else
        {
            if (product.Price > 0)
            {
                return product.Price.ToString("C") + " " + product.QuantityUnitMeasure;
            }
        }
        
        return "R$ 0,00";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
