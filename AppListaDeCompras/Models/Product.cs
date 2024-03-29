using AppListaDeCompras.Models.Enums;

using MongoDB.Bson;

using Realms;

namespace AppListaDeCompras.Models;

public partial class Product : IRealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; }

    [MapTo("name")]
    public string Name { get; set; } = string.Empty;

    [MapTo("quantity")]
    public decimal Quantity { get; set; }

    [MapTo("quantity_unit_measure")]
    public string QuantityUnitMeasure { get; set; } = (string)Enum.GetName(UnitMeasure.Un);

    [MapTo("price")]
    public decimal Price { get; set; }
    
    [MapTo("has_caught")]
    public bool HasCaught { get; set; }
    
    [MapTo("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
}
