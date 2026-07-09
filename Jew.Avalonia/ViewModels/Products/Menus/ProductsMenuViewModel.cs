using System.Collections.ObjectModel;
using Avalonia.Media.Fonts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jew.Applications.ProductInventory.Commands;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Products.Inputs;
using Jew.Avalonia.ViewModels.Products.Services;

namespace Jew.Avalonia.ViewModels.Products.Menus;

public sealed partial class ProductsMenuViewModel : ViewModelBase
{

    public ProductsMenuViewModel
    (SearchProductViewModel categoryFilter, CreateProductCardVM createProductCard, 
    CreateCategoryCardVM createCategoryCard, ModifyProductViewModel modifyProduct)
    {
        CategoryFilter = categoryFilter;
        _createProductCard = createProductCard;
        CreateCategoryCard = createCategoryCard;
        ModifyProduct = modifyProduct;
        ProductCard = _createProductCard;
    }   

    [ObservableProperty]
    private SearchProductViewModel _categoryFilter;
    [ObservableProperty]
    private CreateProductCardVM _createProductCard;
    [ObservableProperty]
    private CreateCategoryCardVM _createCategoryCard;
    
    [ObservableProperty]
    private ModifyProductViewModel _modifyProduct;

    [ObservableProperty]
    private ViewModelBase _productCard;


    [RelayCommand]
    private void ShowCreateProductCard()
    {
        if(ProductCard != CreateProductCard)
            ProductCard = CreateProductCard;
    }

    [RelayCommand]
    private void ShowModifyProductCard()
    {
        if(ProductCard != ModifyProduct)
            ProductCard = ModifyProduct;
    }
}