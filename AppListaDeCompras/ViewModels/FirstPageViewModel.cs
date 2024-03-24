using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppListaDeCompras.ViewModels;

public partial class FirstPageViewModel : ObservableObject
{
    [RelayCommand]
    private void NavigateToAppShell()
    {
        Application.Current.MainPage = new AppShell();
    }
}
