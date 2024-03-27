using AppListaDeCompras.Models.Enums;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AppListaDeCompras.Models;

public partial class Product : ObservableObject
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    
    public UnitMeasure QuantityUnitMeasure { get; set; }
    public decimal Price { get; set; }
    
    [ObservableProperty]
    public bool hasCaught = false;
}
