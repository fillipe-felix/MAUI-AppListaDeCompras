using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Mopups.Services;

namespace AppListaDeCompras.ViewModels.Popups;

public partial class ListToBuySharedPageViewModel : ObservableObject
{
    [ObservableProperty] private ListToBuy _listToBuy = new ListToBuy { Users = new List<User>() };

    [RelayCommand]
    private void Close()
    {
        MopupService.Instance.PopAsync();
    }
}
