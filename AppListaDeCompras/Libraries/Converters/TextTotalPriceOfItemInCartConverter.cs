using System.Globalization;

using AppListaDeCompras.Models;

namespace AppListaDeCompras.Libraries.Converters;

public class TextTotalPriceOfItemInCartConverter : IMultiValueConverter
{

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var product = values[0] as Product;

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
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
