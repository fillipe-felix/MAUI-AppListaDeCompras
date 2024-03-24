using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Mopups.Services;

namespace AppListaDeCompras.ViewModels.Popups;

public partial class ListToBuySharedPageViewModel : ObservableObject
{
    [RelayCommand]
    private void Close()
    {
        MopupService.Instance.PopAsync();
    }
}
