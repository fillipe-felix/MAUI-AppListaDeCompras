using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Email = AppListaDeCompras.Libraries.Utilities.Email;

namespace AppListaDeCompras.ViewModels;

public partial class ProfilePageViewModel : ObservableObject
{
    [ObservableProperty] private User user;

    public ProfilePageViewModel()
    {
        user = new User();
    }

    [RelayCommand]
    private async Task NavigateToAccessCodePage()
    {
        //TODO - Validar dados
        
        
        
        var realm = MongoDbAtlasService.GetMainThreadRealm();
        var userDb = realm.All<User>().FirstOrDefault(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase));

        User.AccessCodeTemp = Text.GerarNumeroAleatorio().ToString();
        User.AccessCodeCreatedAt = DateTime.UtcNow;
        
        if (userDb is null)
        {
            //TODO - Enviar o accessCode por email.
            
            await realm.WriteAsync(() =>
            {
                realm.Add(user);
            });
            
            Email.SendMailWithAccessCode(User);
        }
        else
        {
            //TODO - Enviar o accessCode por email.
            
            await realm.WriteAsync(() =>
            {
                realm.Add(user, true);
            });
            
            Email.SendMailWithAccessCode(User);
        }
        

        var parameters = new Dictionary<string, object>();
        parameters.Add("usuario", user);
        await Shell.Current.GoToAsync("//Profile/AccessCode", parameters);
    }
}
