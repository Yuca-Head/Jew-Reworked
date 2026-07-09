using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        WeakReferenceMessenger.Default.Register<ProductUpdateMessage>(this, async (_, e) => await UpdateProducts(e.ProductsChangedCode));
        WeakReferenceMessenger.Default.Register<CategoryUpdateMessage>(this, async (_, e) => await UpdateCategories(e.CategoriesChangedName));
    }
    
    private readonly InventoryQueryService _service;
    private readonly CategoryQueryService _categoryQuery;
    public ObservableCollection<ProductWithCategoryDto> Products { get; private set;} = [];
    public ObservableCollection<CategoryDto> Categories { get; private set; } = [];

    public event EventHandler? ProductsChanged;

    public event EventHandler? CategoriesChanged;

    public ProductWithCategoryDto? GetProductById(string id)
    => Products.FirstOrDefault(x => x.ProductDto.Code == id);

    private async void UpdateBoth()
    => await Task.WhenAll(UpdateProducts(null),UpdateCategories(null));
    

    private async Task UpdateProducts(IEnumerable<string>? args)
    {
        if(args is null)
            foreach(var p in await _service.GetProductsWithCategories())
                Products.Add(p);     
        else
            foreach(var code in args)
            {
                var vm = Products.FirstOrDefault(x => x.ProductDto.Code==code);
                
                if(vm != default)
                    Products.Remove(vm);
                Products.Add(await _service.GetProductWithCategory(code));
            }

        ProductsChanged?.Invoke(this, EventArgs.Empty);
    }

    private async Task UpdateCategories(IEnumerable<string>? args)
    {
        if(args is null)
            foreach(var c in await _categoryQuery.GetCategories())
                Categories.Add(c);
        else
            foreach(var id in args)
            {
                var vm = Categories.FirstOrDefault(x => x.Name == id);

                if(vm is null)
                    Categories.Add(await _categoryQuery.GetCategoryById(id));
                
                else
                {
                    var item = await _categoryQuery.GetCategoryById(id);

                    vm = item;
                }
            }
        CategoriesChanged?.Invoke(this, EventArgs.Empty);
    }

    
}