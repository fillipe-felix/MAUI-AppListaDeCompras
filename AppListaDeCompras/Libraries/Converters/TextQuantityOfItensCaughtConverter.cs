﻿using System.Globalization;

using AppListaDeCompras.Models;

namespace AppListaDeCompras.Libraries.Converters;

public class TextQuantityOfItensCaughtConverter : IValueConverter
{

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        List<Product> products = (List<Product>)value!;

        var caughtCount = products.Count(p => p.HasCaught);

        return caughtCount > 0 ? $"{caughtCount} itens" : $"{caughtCount} item";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}