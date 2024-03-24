using System.Globalization;

using AppListaDeCompras.Models;

namespace AppListaDeCompras.Libraries.Converters;

public class TextTotalPriceOfItensInCartConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ListToBuy? listToBuy = value as ListToBuy;

        if (listToBuy is null)
        {
            return "R$ 0,00";
        }

        if (listToBuy.Products.Count == 0)
        {
            return "R$ 0,00";
        }

        decimal totalPrice = 0;

        foreach (var product in listToBuy.Products)
        {
            //TODO - Impacto: QuantityUnitMeasure...
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
