using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        await Shell.Current.GoToAsync("//Profile/AccessCode");
    }
}
