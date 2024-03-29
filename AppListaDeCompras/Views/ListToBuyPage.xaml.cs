using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppListaDeCompras.Libraries.Services;

namespace AppListaDeCompras.Views;

public partial class ListToBuyPage : ContentPage
{
    public ListToBuyPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        await MongoDbAtlasService.Init();
        await MongoDbAtlasService.LoginAsync();
    }
}

