using System.Globalization;

using AppListaDeCompras.Models;

namespace AppListaDeCompras.Libraries.Converters;

public class TextTotalPriceOfItensInCartConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        List<Product>? listOfProducts = value as List<Product>;

        if (listOfProducts is null)
        {
            return "R$ 0,00";
        }

        if (listOfProducts.Count == 0)
        {
            return "R$ 0,00";
        }

        decimal totalPrice = 0;

        foreach (var product in listOfProducts)
        {
            if (product.HasCaught)
            {
                totalPrice += product.Price * product.Quantity;
            }
        }

        return totalPrice.ToString("C");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
