using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Models;
using AppListaDeCompras.Models.Enums;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MongoDB.Bson;

using Mopups.Services;

namespace AppListaDeCompras.ViewModels.Popups;

public partial class ListOfItensAddItemPageViewModel : ObservableObject
{
    [ObservableProperty] public Product? product;

    [ObservableProperty]
    private string[] unitsMeasure;

    [ObservableProperty] private ListToBuy _list;

    public ListOfItensAddItemPageViewModel()
    {
        unitsMeasure = Enum.GetNames(typeof(UnitMeasure));
        Product = new Product();
    }
    
    [RelayCommand]
    private void Close()
    {
        MopupService.Instance.PopAsync();
    }
    
    [RelayCommand]
    private async Task Save()
    {
        //TODO - Validar os dados
        
        //TODO - Salvar as informações
        var realm = MongoDbAtlasService.GetMainThreadRealm();

        await realm.WriteAsync(() =>
        {
            if (Product!.Id == default)
            {
                Product.Id = ObjectId.GenerateNewId();
                Product.CreatedAt = DateTime.UtcNow;
                List.Products.Add(Product);
                realm.Add(List, true);
            }
            else
            {
                //TODO - Update
                realm.Add(List, true);
            }
        });
        
        await MopupService.Instance.PopAsync();
    }
}
