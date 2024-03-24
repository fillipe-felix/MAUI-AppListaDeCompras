namespace AppListaDeCompras.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string AccessCodeTemp { get; set; } = string.Empty;
    public DateTimeOffset AccessCodeCreatedAt { get; set; }
}
