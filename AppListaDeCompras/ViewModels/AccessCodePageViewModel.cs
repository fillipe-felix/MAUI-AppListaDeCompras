using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

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

            await AppShell.Current.GoToAsync("../");
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Alerta", "Código de acesso inválido!", "Ok");

            AccessCode = string.Empty;
            
            return;
        }
    }
}
