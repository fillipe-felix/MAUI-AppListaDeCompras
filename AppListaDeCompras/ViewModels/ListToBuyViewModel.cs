﻿using System.Collections.ObjectModel;

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
                        Name = "Arroz",
                        Price = 28.99m,
                        Quantity = 2,
                        HasCaught = true
                    },
                    new Product
                    {
                        Name = "Feijão",
                        Price = 7.49m,
                        Quantity = 3,
                        HasCaught = true
                    },
                    new Product
                    {
                        Name = "Leite condensado",
                        Price = 6.29m,
                        Quantity = 1
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
                        Name = "Arroz",
                        Price = 36.99m,
                        Quantity = 2,
                        HasCaught = true
                    },
                    new Product
                    {
                        Name = "Feijão",
                        Price = 8.49m,
                        Quantity = 2,
                        HasCaught = true
                    },
                    new Product
                    {
                        Name = "Leite condensado",
                        Price = 6.29m,
                        Quantity = 1,
                        HasCaught = true
                    }
                }
            }
        };
    }
}