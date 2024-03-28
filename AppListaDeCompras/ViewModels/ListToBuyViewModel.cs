using System.Collections.ObjectModel;

using AppListaDeCompras.Models;
using AppListaDeCompras.Models.Enums;
using AppListaDeCompras.Views.Popups;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Mopups.Services;

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
                        QuantityUnitMeasure = UnitMeasure.Un,
                        HasCaught = true
                    },
                    new Product
                    {
                        Name = "Feijão",
                        Price = 7.49m,
                        Quantity = 3,
                        QuantityUnitMeasure = UnitMeasure.Un,
                        HasCaught = true
                    },
                    new Product
                    {
                        Name = "Leite condensado",
                        Price = 6.29m,
                        QuantityUnitMeasure = UnitMeasure.Un,
                        Quantity = 1
                    },
                    new Product
                    {
                        Name = "Coxão Mole",
                        Price = 29.80m,
                        QuantityUnitMeasure = UnitMeasure.Kg,
                        Quantity = 1.5m
                    },
                    new Product
                    {
                        Name = "Alcatra",
                        QuantityUnitMeasure = UnitMeasure.Kg,
                        Quantity = 1.5m
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
                        HasCaught = true,
                        QuantityUnitMeasure = UnitMeasure.Un
                    },
                    new Product
                    {
                        Name = "Feijão",
                        Price = 8.49m,
                        Quantity = 2,
                        QuantityUnitMeasure = UnitMeasure.Un,
                        HasCaught = true
                    },
                    new Product
                    {
                        Name = "Leite condensado",
                        Price = 6.29m,
                        Quantity = 1,
                        QuantityUnitMeasure = UnitMeasure.Un,
                        HasCaught = true
                    }
                }
            }
        };
    }
    
    [RelayCommand]
    private void OpenPopupSharedPage(ListToBuy listSelected)
    {
        MopupService.Instance.PushAsync(new ListToBuySharedPage(listSelected));
    }

    [RelayCommand]
    private async Task OpenListOfItensPage(ListToBuy selectedList)
    {
        var pageParameter = new Dictionary<string, object>
        {
            { "ListToBuy", selectedList }
        };
        
        await Shell.Current.GoToAsync("//ListToBuy/ListOfItens", pageParameter);
    }

    [RelayCommand]
    private async Task OpenAddListOfItensPage()
    {
        await Shell.Current.GoToAsync("//ListToBuy/ListOfItens");
    }
}
