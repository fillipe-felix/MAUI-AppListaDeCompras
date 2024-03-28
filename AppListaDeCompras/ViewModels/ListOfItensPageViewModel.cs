using AppListaDeCompras.Models;
using AppListaDeCompras.Views.Popups;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Mopups.Services;

namespace AppListaDeCompras.ViewModels;

[QueryProperty(nameof(ListToBuy), "ListToBuy")]
public partial class ListOfItensPageViewModel : ObservableObject
{

    private ListToBuy? _listToBuy;
    public ListToBuy? ListToBuy
    {
        get => _listToBuy;
        set => SetProperty(ref _listToBuy, value);
        
    }

    [RelayCommand]
    private void UpdateListToBuy()
    {
        OnPropertyChanged(nameof(ListToBuy));
    }

    [RelayCommand]
    private async Task BackPage()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    [RelayCommand]
    private async Task OpenPopupAddItemPage()
    {
        await MopupService.Instance.PushAsync(new ListOfItensAddItemPage());
    }
}
