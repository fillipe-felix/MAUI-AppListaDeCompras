namespace AppListaDeCompras.Models;

public class ListToBuy
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public IList<Product> Products { get; set; } = new List<Product>();
    public IList<User> Users { get; set; } = new List<User>();
    
    public DateTimeOffset CreatedAt { get; set; }
}
