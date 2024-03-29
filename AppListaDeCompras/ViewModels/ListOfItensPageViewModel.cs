using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Models;
using AppListaDeCompras.Views.Popups;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

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

        WeakReferenceMessenger.Default.Register<string>(string.Empty, ((recipient, message) =>
        {
            UpdateListToBuy();
        }));
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
            if (ListToBuy.Id == default(ObjectId))
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
    private async Task BackPage()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    [RelayCommand]
    private async Task OpenPopupAddItemPage()
    {
        await MopupService.Instance.PushAsync(new ListOfItensAddItemPage(ListToBuy!));
    }
    
    [RelayCommand]
    private async Task OpenPopupEditItemPage(Product product)
    {
        await MopupService.Instance.PushAsync(new ListOfItensAddItemPage(ListToBuy!, product));
    }

    [RelayCommand]
    private async Task DeleteItem(Product product)
    {
        var realm = MongoDbAtlasService.GetMainThreadRealm();

        await realm.WriteAsync(() =>
        {
            realm.Remove(product);
        });
    }

    [RelayCommand]
    private void UpdateListToBuy()
    {
        OnPropertyChanged(nameof(ListToBuy));
    }
}
