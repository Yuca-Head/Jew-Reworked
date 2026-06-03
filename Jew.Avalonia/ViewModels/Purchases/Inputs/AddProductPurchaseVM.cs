using CommunityToolkit.Mvvm.ComponentModel;

namespace Jew.Avalonia.ViewModels.Purchases.Inputs;

//Posible cambio para las propiedades no modificables como ItemNumber, Nombre y Categoria.
public partial class AddProductPurchaseVM : ViewModelBase
{
    [ObservableProperty]
    private int _itemNumber;
    [ObservableProperty]
    private string _name ="";
    [ObservableProperty]
    private string _categoryName = "";
    [ObservableProperty]
    private int _quantity;
    [ObservableProperty]
    private decimal _unitCost;
    [ObservableProperty]
    private decimal _subTotal;


    partial void OnQuantityChanged(int oldValue, int newValue)
    {
        if(newValue != oldValue)
            SubTotal = UnitCost * Quantity;
    }

    partial void OnUnitCostChanged(decimal oldValue, decimal newValue)
    {
        if(newValue != oldValue)
            SubTotal = UnitCost * Quantity;
    }
}