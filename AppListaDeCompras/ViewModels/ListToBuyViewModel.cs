using System.Collections.ObjectModel;

using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Models;
using AppListaDeCompras.Models.Enums;
using AppListaDeCompras.Views.Popups;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Mopups.Services;

using Realms;

namespace AppListaDeCompras.ViewModels;

public partial class ListToBuyViewModel : ObservableObject
{
    [ObservableProperty]
    private IQueryable<ListToBuy> _listsOfListToBuy;

    public ListToBuyViewModel()
    {
        
    }

    [RelayCommand]
    private async Task OnAppearing()
    {
        await MongoDbAtlasService.Init();
        await MongoDbAtlasService.LoginAsync();
        
        //TODO - carregar os dados

        var realm = MongoDbAtlasService.GetMainThreadRealm();

        ListsOfListToBuy = realm.All<ListToBuy>();
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
