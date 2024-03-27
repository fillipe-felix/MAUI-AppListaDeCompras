using CommunityToolkit.Mvvm.ComponentModel;

namespace AppListaDeCompras.Models;

public partial class Product : ObservableObject
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    
    //TODO - trocar para um ENUM
    public string QuantityUnitMeasure { get; set; }
    public decimal Price { get; set; }
    
    [ObservableProperty]
    public bool hasCaught = false;
}
