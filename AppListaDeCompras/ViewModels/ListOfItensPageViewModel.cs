using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
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
        set
        {
            ListToBuyName = value.Name;
            SetProperty(ref _listToBuy, value);
        }

    }

    [ObservableProperty]
    private string _listToBuyName;

    public ListOfItensPageViewModel()
    {
        ListToBuy = new ListToBuy();

        if (!WeakReferenceMessenger.Default.IsRegistered<string>("NewItem"))
        {
            WeakReferenceMessenger.Default.Register<string>("NewItem", ((recipient, message) =>
            {
                UpdateListToBuy();
            }));
        }
    }

    [RelayCommand]
    private async Task SaveListToBuy()
    {
        if (string.IsNullOrWhiteSpace(ListToBuyName))
        {
            App.Current.MainPage.DisplayAlert("Validação", "O nome da lista deve ser preenchido", "Ok");
            return;
        }
        
        var realm = MongoDbAtlasService.GetMainThreadRealm();

        await realm.WriteAsync(() =>
        {
            if (ListToBuy.Id == default(ObjectId))
            {
                ListToBuy.Id = ObjectId.GenerateNewId();
                ListToBuy.Name = ListToBuyName;
                ListToBuy.CreatedAt = DateTime.UtcNow;
                
                //Mongo -> App Services -> App users (User Anonymous)
                

                if (UserLoggedManager.ExistsUser())
                {
                    var user = UserLoggedManager.GetUser();

                    var userDb = realm.All<User>().FirstOrDefault(u => u.Id == user.Id);

                    if (userDb is not null)
                    {
                        ListToBuy.Users.Add(userDb);
                    }
                }
                else
                {
                    ListToBuy.AnonymousUserId = new ObjectId(MongoDbAtlasService.CurrentUser.Id);
                }

                realm.Add(ListToBuy);
            }
            else
            {
                ListToBuy.Name = ListToBuyName;
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
