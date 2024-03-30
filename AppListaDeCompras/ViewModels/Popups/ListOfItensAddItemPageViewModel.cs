using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Libraries.Validations;
using AppListaDeCompras.Models;
using AppListaDeCompras.Models.Enums;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using MongoDB.Bson;

using Mopups.Services;

namespace AppListaDeCompras.ViewModels.Popups;

public partial class ListOfItensAddItemPageViewModel : ObservableObject
{
    private Product? _product;
    public Product? Product
    {
        get { return _product;}
        set { 
            _product = value;
            ProductForm = new Product
            {
                Id = value.Id,
                Name = value.Name,
                Price = value.Price,
                Quantity = value.Quantity,
                QuantityUnitMeasure = value.QuantityUnitMeasure,
                HasCaught = value.HasCaught,
                CreatedAt = value.CreatedAt
            };
        }
    }
    
    [ObservableProperty] public Product? productForm;

    [ObservableProperty]
    private string[] unitsMeasure;

    [ObservableProperty] private ListToBuy _list;
    
    [ObservableProperty]
    private string _errorsMessage;

    private AddItemValidator _validator;

    public ListOfItensAddItemPageViewModel()
    {
        unitsMeasure = Enum.GetNames(typeof(UnitMeasure));
        Product = new Product();
        ProductForm = new Product();
        _validator = App.Current.MainPage.Handler.MauiContext.Services.GetRequiredService<AddItemValidator>();
    }
    
    [RelayCommand]
    private void Close()
    {
        MopupService.Instance.PopAsync();
    }
    
    [RelayCommand]
    private async Task Save()
    {
        ErrorsMessage = string.Empty;
        var validateResult = _validator.Validate(ProductForm!);
        
        if (!validateResult.IsValid)
        {
            ErrorsMessage = Validator.ShowErrorMessage(validateResult);
            return;
        }
        
        var realm = MongoDbAtlasService.GetMainThreadRealm();

        await realm.WriteAsync(() =>
        {
            if (Product!.Id == default(ObjectId))
            {
                ProductForm.Id = ObjectId.GenerateNewId();
                ProductForm.CreatedAt = DateTime.UtcNow;
                
                List.Products.Add(ProductForm);
                realm.Add(List);

                // Trabalha como Pub Sub
                //Aqui esta enviando a notificação.
                WeakReferenceMessenger.Default.Send(string.Empty);
            }
            else
            {
                Product.Name = ProductForm.Name;
                Product.Quantity = ProductForm.Quantity;
                Product.Price = ProductForm.Price;
                Product.QuantityUnitMeasure = ProductForm.QuantityUnitMeasure;
            }
        });
        
        await MopupService.Instance.PopAsync();
    }
}
