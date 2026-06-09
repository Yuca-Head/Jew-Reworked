using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Avalonia.Shared;
using Jew.Avalonia.ViewModels.Products.Outputs;
using Jew.Domain.Purchases.Transactions;

namespace Jew.Avalonia.ViewModels.Purchases.Inputs;

//Posible cambio para las propiedades no modificables como ItemNumber, Nombre y Categoria.
public partial class AddProductPurchaseVM() : ViewModelBase
{
    public AddProductPurchaseVM(ProductViewModelBase @base) : this()
    {
        Name = @base.Name;
        Code = @base.Code;
        CategoryName = @base.Category?.Name ?? "";
    }
    [ObservableProperty]
    private int _itemNumber;
    [ObservableProperty]
    private string _name ="";
    [ObservableProperty]
    private string _categoryName = "";
    [ObservableProperty]
    private string _code = "";
    [ObservableProperty]
    private int _quantity;
    [ObservableProperty]
    private decimal _unitCost;
    [ObservableProperty]
    private decimal _subTotal;

    partial void OnQuantityChanged(int oldValue, int newValue)
    {
        if(newValue != oldValue)
            SubTotal = (decimal)(UnitCost * Quantity);
    }

    partial void OnUnitCostChanged(decimal oldValue, decimal newValue)
    {
        if(newValue != oldValue)
            SubTotal = (decimal)(UnitCost * Quantity);
    }

}
