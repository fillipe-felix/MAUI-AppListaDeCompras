using System.Collections.ObjectModel;

using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AppListaDeCompras.ViewModels;

public partial class ListToBuyViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ListToBuy> _listToBuy;

    public ListToBuyViewModel()
    {
        _listToBuy = new ObservableCollection<ListToBuy>
        {
            new ListToBuy
            {
                Name = "Minhas lista",
                Users = new List<User>
                {
                    new User
                    {
                        Name = "Fillipe",
                        Email = "fillipe.felix@teste.com"
                    },
                    new User
                    {
                        Name = "Fabiane",
                        Email = "fabiane@teste.com"
                    }
                },
                Products = new List<Product>
                {
                    new Product
                    {
                    },
                    new Product
                    {
                    },
                    new Product
                    {
                    }
                }
            },
            new ListToBuy
            {
                Name = "Minhas lista 2",
                Users = new List<User>
                {
                    new User
                    {
                        Name = "Fillipe",
                        Email = "fillipe.felix@teste.com"
                    }
                },
                Products = new List<Product>
                {
                    new Product
                    {
                    },
                    new Product
                    {
                    }
                }
            }
        };
    }
}
