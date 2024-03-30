using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using MongoDB.Bson;

namespace AppListaDeCompras.ViewModels;

[QueryProperty(nameof(User), "usuario")]
public partial class AccessCodePageViewModel : ObservableObject
{
    [ObservableProperty]
    private User _user;
    
    [ObservableProperty]
    private string _accessCode;

    [RelayCommand]
    private async Task VerifyAccessCode()
    {
        if (AccessCode.Equals(User.AccessCodeTemp))
        {
            var finalDate = User.AccessCodeCreatedAt.AddMinutes(5);

            if (DateTime.UtcNow > finalDate)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Código de acesso expirado!", "Ok");
                return;
            }

            UserLoggedManager.SetUser(User);

            WeakReferenceMessenger.Default.Send("Logado");

            TransferAllListToBuyAnonymousToUserLogged(User);

            await AppShell.Current.GoToAsync("../");
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Alerta", "Código de acesso inválido!", "Ok");
            AccessCode = string.Empty;
            return;
        }
    }
    
    private void TransferAllListToBuyAnonymousToUserLogged(User userLogged)
    {
        var realm = MongoDbAtlasService.GetMainThreadRealm();
        var userLoggedId = new ObjectId(MongoDbAtlasService.CurrentUser.Id);
        var listToBuy = realm.All<ListToBuy>().Where(a => a.AnonymousUserId == userLoggedId).ToList();

        realm.WriteAsync(() =>
        {
            foreach (var list in listToBuy)
            {
                list.AnonymousUserId = default(ObjectId);
                list.Users.Add(userLogged);
            }
        });
    }
}
