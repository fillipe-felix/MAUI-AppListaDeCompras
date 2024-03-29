using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppListaDeCompras.Models;
using AppListaDeCompras.ViewModels.Popups;

using Mopups.Pages;

namespace AppListaDeCompras.Views.Popups;

public partial class ListOfItensAddItemPage : PopupPage
{
    /// <summary>
    /// Constrututor usado para a cadastro do produto
    /// </summary>
    public ListOfItensAddItemPage(ListToBuy listToBuy)
    {
        InitializeComponent();

        var vm = (ListOfItensAddItemPageViewModel)BindingContext;

        vm.List = listToBuy;
    }

    /// <summary>
    /// Constrututor usado para a edição do produto
    /// </summary>
    public ListOfItensAddItemPage(ListToBuy listToBuy, Product product)
    {
        InitializeComponent();
        
        var vm = (ListOfItensAddItemPageViewModel)BindingContext;

        vm.List = listToBuy;
        vm.Product = product;
    }
}

