using AppListaDeCompras.Views;

namespace AppListaDeCompras;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //Mostra essa tela apenas 1x
        if (VersionTracking.IsFirstLaunchEver)
        {
            MainPage = new FirstPage();
        }
        else
        {
            MainPage = new AppShell();
        }
    }
}
