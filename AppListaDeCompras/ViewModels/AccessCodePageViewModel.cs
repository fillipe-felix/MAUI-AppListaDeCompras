using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppListaDeCompras.ViewModels;

public partial class AccessCodePageViewModel : ObservableObject
{
    [ObservableProperty]
    private User _user;

    [RelayCommand]
    private void VerifyAccessCode()
    {
        throw new NotImplementedException();
    }
}
