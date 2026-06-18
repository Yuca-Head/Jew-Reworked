using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Queries;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.ViewModels;
using Jew.Avalonia.ViewModels.Categories.Outputs;
using Jew.Avalonia.ViewModels.Products.Outputs;

namespace Jew.Avalonia.Shared.Contexts;

public sealed partial class ProductState
{
    public ProductState(InventoryQueryService inventoryQueryService, CategoryQueryService categoryQuery)
    {  
        _service = inventoryQueryService;
        _categoryQuery = categoryQuery;
        UpdateBoth();
        WeakReferenceMessenger.Default.Register<ProductUpdateMessage>(this, (_, e) => UpdateProducts(e.ProductsChangedCode));
        WeakReferenceMessenger.Default.Register<CategoryUpdateMessage>(this, (_, e) => UpdateCategories(e.CategoriesChangedName));
    }
    
    private readonly InventoryQueryService _service;
    private readonly CategoryQueryService _categoryQuery;
    public ObservableCollection<ProductWithCategoryDto> Products { get; private set;} = [];
    public ObservableCollection<CategoryDto> Categories { get; private set; } = [];

    public ProductWithCategoryDto? GetProductById(string id)
    => Products.FirstOrDefault(x => x.ProductDto.Code == id);

    public void UpdateBoth()
    {
        UpdateProducts(null);
        UpdateCategories(null);
    }

    public void UpdateProducts(IEnumerable<string>? args)
    {
        if(args is null)
            foreach(var p in _service.GetProductsWithCategories())
                Products.Add(p);     
        else
            foreach(var code in args)
            {
                var vm = Products.FirstOrDefault(x => x.ProductDto.Code==code);

                if(vm is null)
                {
                    Products.Add(_service.GetProductWithCategory(code));
                    return;
                }
                var item = _service.GetProductInventory(code);

                vm = item.Product;
            }
    }

    public void UpdateCategories(IEnumerable<string>? args)
    {
        if(args is null)
            foreach(var c in _categoryQuery.GetCategories())
                Categories.Add(c);
        else
            foreach(var id in args)
            {
                var vm = Categories.FirstOrDefault(x => x.Name == id);

                if(vm is null)
                {
                    Categories.Add(_categoryQuery.GetCategoryById(id));
                    return;
                }
                var item = _categoryQuery.GetCategoryById(id);

                vm = item;
            }
    }
}