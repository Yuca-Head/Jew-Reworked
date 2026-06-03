using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.Queries;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Products.Outputs;

namespace Jew.Avalonia.ViewModels.Products.Services;

public partial class SearchProductViewModel : ViewModelBase
{

    public SearchProductViewModel(InventoryState inventoryState)
    {
        _inventoryState = inventoryState;

        CategoriesFilter = ["Todos", .._inventoryState.Categories.Select(x => x.Name)];
        
        ApplyFilter();
    }
    private readonly InventoryState _inventoryState;
    
    //Filtered list.
    public ObservableCollection<ProductViewModelBase> DisplayList {get; set;} = [];
   
    [ObservableProperty]
    private ObservableCollection<string> _categoriesFilter = [];

    [ObservableProperty]
    private string _selectedCategory = "Todos";

    private void ApplyFilter()
    {
        var products =
            _inventoryState.Products;   

        if (SelectedCategory != "Todos")
            products = [..products.Where(
                p => p.CategoryDto.Name == SelectedCategory)];

        DisplayList.Clear();

        foreach(var p in products)
            DisplayList.Add(new ProductViewModelBase(p));
    }   

    partial void OnSelectedCategoryChanged(string? oldValue, string newValue)
    {
        ApplyFilter();
    }

}