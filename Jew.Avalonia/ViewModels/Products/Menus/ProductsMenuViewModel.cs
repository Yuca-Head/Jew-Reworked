using System.Collections.ObjectModel;
using Avalonia.Media.Fonts;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.Commands;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Products.Services;

namespace Jew.Avalonia.ViewModels.Products.Menus;

public sealed partial class ProductsMenuViewModel : ViewModelBase
{

    public ProductsMenuViewModel
    (SearchProductViewModel categoryFilter, CreateProductCardVM createProductCard, CreateCategoryCardVM createCategoryCard)
    {
        CategoryFilter = categoryFilter;
        CreateProductCard = createProductCard;
        CreateCategoryCard = createCategoryCard;
    }   

    [ObservableProperty]
    private SearchProductViewModel _categoryFilter;

    [ObservableProperty]
    private CreateProductCardVM _createProductCard;
    [ObservableProperty]
    private CreateCategoryCardVM _createCategoryCard;

}