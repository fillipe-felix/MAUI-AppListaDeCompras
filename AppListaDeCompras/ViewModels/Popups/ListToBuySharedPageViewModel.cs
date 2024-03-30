using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Mopups.Services;

namespace AppListaDeCompras.ViewModels.Popups;

public partial class ListToBuySharedPageViewModel : ObservableObject
{
    [ObservableProperty] private string? _email;
    
    [ObservableProperty] private ListToBuy _listToBuy = new ListToBuy();

    [RelayCommand]
    private void Close()
    {
        MopupService.Instance.PopAsync();
    }

    [RelayCommand]
    private void Add()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            App.Current.MainPage.DisplayAlert("Erro", "Preencha o campo e-mail!", "Ok");
            return;
        }

        var realm = MongoDbAtlasService.GetMainThreadRealm();
        var user = realm.All<User>()
            .FirstOrDefault(e => e.Email.Equals(Email, StringComparison.OrdinalIgnoreCase));

        if (user is null)
        {
            App.Current.MainPage.DisplayAlert("Não localizado", "Usuário não localizado com o e-mail informado!", "Ok");
            return;
        }

        realm.WriteAsync(() =>
        {
            ListToBuy.Users.Add(user);
        });
    }
}
