using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Avalonia.ViewModels.Categories.Outputs;

namespace Jew.Avalonia.ViewModels.Products.Outputs;

public partial class ProductSaleViewModel : ProductViewModelBase
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SubTotal))]
    private decimal _price;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SubTotal))]
    private int _stock;

    
    public decimal SubTotal
    => Price * Stock;

    public ProductSaleViewModel
    (ProductViewModelBase product, decimal price, int stock) :
    base(product)
    {
        Stock = stock;
        Price = price;
    }

    public ProductSaleViewModel(ProductViewModelBase product)
    :base(product){}
}