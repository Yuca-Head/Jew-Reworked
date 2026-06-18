using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.Queries;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Products.Outputs;

namespace Jew.Avalonia.ViewModels.Products.Services;

/// <summary>
/// Actually it is just a Category Filter.
/// </summary>
public partial class SearchProductViewModel : ViewModelBase
{

    public static SearchProductViewModel Clone(SearchProductViewModel original)
    => new(original._inventoryState, original.ExcludeDefaultValue);
    public SearchProductViewModel(ProductState productState, bool excludeDefault = false)
    {
        _inventoryState = productState;
        
        _inventoryState.CategoriesChanged  += (_,_) => Update();
        _inventoryState.ProductsChanged += (_,_) => ApplyFilter();
        ExcludeDefaultValue = excludeDefault;
        Update();
    }


    [ObservableProperty]
    private bool _excludeDefaultValue;

    private readonly ProductState _inventoryState;
    
    //Filtered list.
    public ObservableCollection<ProductViewModelBase> DisplayList {get; set;} = [];

    public const string DefaultValue = "Todos";
   
    
    public ObservableCollection<string> CategoriesFilter {get;} = [];

    [ObservableProperty]
    private string _selectedCategory = DefaultValue;

    private void ApplyFilter()
    {
        var products = _inventoryState.Products;   

        if (SelectedCategory != DefaultValue)
            products = [..products.Where(
                p => p.CategoryDto.Name == SelectedCategory)];

        DisplayList.Clear();

        foreach(var p in products)
        {
            DisplayList.Add(new ProductViewModelBase(p));
        }
    }   

    partial void OnSelectedCategoryChanged(string? oldValue, string newValue)
    {
        ApplyFilter();
    }

    partial void OnExcludeDefaultValueChanged(bool value)
    {
        Update();
    }
    private void Update()
    {
        var temp = SelectedCategory;
        CategoriesFilter.Clear();
        foreach(var c in ExcludeDefaultValue ? 
        _inventoryState.Categories.Select(x => x.Name)  :
        [DefaultValue, .._inventoryState.Categories.Select(x => x.Name)])
            CategoriesFilter.Add(c);
        
        SelectedCategory = temp;
        ApplyFilter();
    }

}