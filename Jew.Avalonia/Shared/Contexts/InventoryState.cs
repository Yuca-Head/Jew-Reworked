using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Queries;
using Jew.Avalonia.ViewModels;
using Jew.Avalonia.ViewModels.Categories.Outputs;
using Jew.Avalonia.ViewModels.Products.Outputs;

namespace Jew.Avalonia.Shared.Contexts;

public sealed partial class InventoryState : ViewModelBase
{
    public InventoryState(InventoryQueryService inventoryQueryService, CategoryQueryService categoryQuery)
    {  
        _service = inventoryQueryService;
        _categoryQuery = categoryQuery;
        Update();
    }
    
    private readonly InventoryQueryService _service;
    private readonly CategoryQueryService _categoryQuery;
    public ObservableCollection<ProductWithCategoryDto> Products { get; private set;} = [];
    public ObservableCollection<CategoryDto> Categories { get; private set; } = [];


    public void Update()
    {
        Products.Clear();
        Categories.Clear();
        Products = [.._service.GetProductsWithCategories()];
        Categories = [.._categoryQuery.GetCategories()];
    }
}