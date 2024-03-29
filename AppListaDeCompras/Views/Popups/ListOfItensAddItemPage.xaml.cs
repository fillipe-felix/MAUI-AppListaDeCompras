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
    public ListOfItensAddItemPage(ListToBuy listToBuy)
    {
        InitializeComponent();

        var vm = (ListOfItensAddItemPageViewModel)BindingContext;

        vm.List = listToBuy;
    }
}

