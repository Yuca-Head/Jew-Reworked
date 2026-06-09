using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Avalonia.ViewModels.Products.Outputs;

namespace Jew.Avalonia.ViewModels.Suppliers.Outputs;

public partial class SupplierProductViewModel(SupplierViewModel supplierViewModel, ProductViewModelBase productViewModel) : ViewModelBase
{
    [ObservableProperty]
    private SupplierViewModel _supplier = supplierViewModel;
    [ObservableProperty]
    private ProductViewModelBase _product = productViewModel;

    public static SupplierProductViewModel From(SupplierProductDto suppProductDto)
    => new(SupplierViewModel.From(suppProductDto.Supplier), 
    new(suppProductDto.Product, new(suppProductDto.Product.CategoryId, "")));
}