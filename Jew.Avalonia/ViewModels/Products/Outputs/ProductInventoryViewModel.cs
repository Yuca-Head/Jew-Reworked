using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Avalonia.ViewModels.Categories.Outputs;

namespace Jew.Avalonia.ViewModels.Products.Outputs;

public partial class ProductInventoryViewModel : ProductDetailsViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    private decimal _averageCost;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    private int _stock;
    public ProductInventoryViewModel(ProductInventoryDto product) : 
    base(product.Product.ProductDto, CategoryViewModel.From(product.Product.CategoryDto))
    {
        AverageCost = product.Cost;
        Stock = product.Stock;
    }
    public ProductInventoryViewModel(ProductDetailsViewModel product) : base(product){}
        
    public decimal Total => AverageCost * Stock;
}