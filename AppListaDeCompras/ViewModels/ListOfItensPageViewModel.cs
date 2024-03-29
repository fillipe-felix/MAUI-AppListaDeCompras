using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Models;
using AppListaDeCompras.Views.Popups;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MongoDB.Bson;

using Mopups.Services;

namespace AppListaDeCompras.ViewModels;

[QueryProperty(nameof(ListToBuy), "ListToBuy")]
public partial class ListOfItensPageViewModel : ObservableObject
{

    private ListToBuy? _listToBuy;
    public ListToBuy? ListToBuy
    {
        get => _listToBuy;
        set => SetProperty(ref _listToBuy, value);
        
    }

    public ListOfItensPageViewModel()
    {
        ListToBuy = new ListToBuy();
    }

    [RelayCommand]
    private async Task SaveListToBuy()
    {
        if (string.IsNullOrWhiteSpace(ListToBuy.Name))
        {
            return;
        }
        
        var realm = MongoDbAtlasService.GetMainThreadRealm();

        await realm.WriteAsync(() =>
        {
            if (ListToBuy.Id == default)
            {
                ListToBuy.Id = ObjectId.GenerateNewId();
                ListToBuy.CreatedAt = DateTime.UtcNow;

                realm.Add(ListToBuy);
            }
            else
            {
                realm.Add(ListToBuy, true);
            }
        });
    }

    [RelayCommand]
    private void UpdateListToBuy()
    {
        OnPropertyChanged(nameof(ListToBuy));
    }

    [RelayCommand]
    private async Task BackPage()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    [RelayCommand]
    private async Task OpenPopupAddItemPage()
    {
        await MopupService.Instance.PushAsync(new ListOfItensAddItemPage(ListToBuy));
    }
}
