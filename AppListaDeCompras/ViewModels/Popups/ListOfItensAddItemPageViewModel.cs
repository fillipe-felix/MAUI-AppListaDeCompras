using AppListaDeCompras.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Mopups.Services;

namespace AppListaDeCompras.ViewModels.Popups;

public partial class ListOfItensAddItemPageViewModel : ObservableObject
{
    [ObservableProperty] public Product? product;
    
    [RelayCommand]
    private void Close()
    {
        MopupService.Instance.PopAsync();
    }
    
    [RelayCommand]
    private void Save()
    {
        
    }
}
