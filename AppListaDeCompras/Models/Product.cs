namespace AppListaDeCompras.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    
    //TODO - trocar para um ENUM
    public string QuantityUnitMeasure { get; set; }
    public decimal Price { get; set; }
    public bool HasCaught { get; set; } = false;
}
