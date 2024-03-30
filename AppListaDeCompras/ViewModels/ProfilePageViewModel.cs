using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using MongoDB.Bson;

using Email = AppListaDeCompras.Libraries.Utilities.Email;

namespace AppListaDeCompras.ViewModels;

public partial class ProfilePageViewModel : ObservableObject
{
    [ObservableProperty] private string _textUserLogged;
    [ObservableProperty] private bool _isLogged;
    [ObservableProperty] private User _user;

    public ProfilePageViewModel()
    {
        _user = new User(); 

        GetLoggedUserMessage();

        if (!WeakReferenceMessenger.Default.IsRegistered<string>("Logado"))
        {
            WeakReferenceMessenger.Default.Register("Logado", (object obj, string str) =>
            {
                GetLoggedUserMessage();
            });
        }
    }

    private void GetLoggedUserMessage()
    {
        IsLogged = UserLoggedManager.ExistsUser();

        if (IsLogged)
        {
            var user = UserLoggedManager.GetUser();
            TextUserLogged = $"Usuário logado! {user.Name} ({user.Email})";
        }
    }

    [RelayCommand]
    private async Task NavigateToAccessCodePage()
    {
        var realm = MongoDbAtlasService.GetMainThreadRealm();
        var userDb = realm.All<User>().FirstOrDefault(u => u.Email.Equals(_user.Email, StringComparison.OrdinalIgnoreCase));

       
        if (userDb is null)
        {
            await realm.WriteAsync(() =>
            {
                User.AccessCodeTemp = Text.GerarNumeroAleatorio().ToString();
                User.AccessCodeCreatedAt = DateTime.UtcNow;
                User.CreatedAt = DateTime.UtcNow;
                User.Id = ObjectId.GenerateNewId();
                realm.Add(User);
            });
            
            Email.SendMailWithAccessCode(User);
        }
        else
        {
            await realm.WriteAsync(() =>
            {
                User.Id = userDb.Id;
                User.CreatedAt = userDb.CreatedAt;
                User.Name = userDb.Name;
                User.AccessCodeTemp = Text.GerarNumeroAleatorio().ToString();
                User.AccessCodeCreatedAt = DateTime.UtcNow;
                realm.Add(User, true);
            });
            
            Email.SendMailWithAccessCode(User);
        }
        

        var parameters = new Dictionary<string, object>();
        parameters.Add("usuario", User);
        await Shell.Current.GoToAsync("//Profile/AccessCode", parameters);
    }

    [RelayCommand]
    private void Logout()
    {
        UserLoggedManager.RemoveUser();
        User = new User();
        IsLogged = false;
    }
}
