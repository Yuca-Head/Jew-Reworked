using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Queries;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Products.Outputs;
using Jew.Avalonia.ViewModels.Products.Services;

namespace Jew.Avalonia.ViewModels.Inventory.Menus;

public partial class InventoryMenuViewModel : ViewModelBase
{
   public InventoryMenuViewModel
    (ProductState productState, InventoryState inventoryState)
    {
        CatFilter = new(productState);
        _inventoryState = inventoryState;
        _inventoryState.StateChanged += (_, _) => 
        {
            ApplyFilters();
            UpdateUnfilteredInfo();
        };
        CatFilter.PropertyChanged += (_,e) =>
        {
            if(e.PropertyName is nameof(CatFilter.SelectedCategory))
            {
                ApplyFilters();
            }
        };

        ApplyFilters();
        UpdateUnfilteredInfo();
    }


    private readonly InventoryState _inventoryState;
    public ObservableCollection<ProductInventoryViewModel> DisplayedProducts{get; private set;} = [];

    [ObservableProperty]
    private decimal _totalInventory;

    [ObservableProperty]
    public decimal _totalStock;
    

    #region Filters

    
    public ObservableCollection<ProductInventoryViewModel> Products => _inventoryState.Products;

    [ObservableProperty]
    private SearchProductViewModel _catFilter;

    [ObservableProperty]
    private string _searchCode = "";

    [ObservableProperty]
    private bool _lowStockOnly = false;

    [ObservableProperty]
    private int _productsWithoutExistence;
    [ObservableProperty]
    private int _lowStockCount;

    private static 
    Func<IEnumerable<ProductInventoryViewModel>, IEnumerable<ProductInventoryViewModel>> GetLowStock()
    => (query) => query.Where(x => x.Stock <= 15);

    partial void OnLowStockOnlyChanged(bool oldValue, bool newValue)
    {
        ApplyFilters();
    }

    
    partial void OnSearchCodeChanged(string? oldValue, string newValue)
    {
        if(oldValue == newValue)
            return;
        ApplyFilters();
    }

    private IEnumerable<ProductInventoryViewModel>  SearchbarFilter(IEnumerable<ProductInventoryViewModel> query)
    {
        if(string.IsNullOrWhiteSpace(SearchCode))
            return query;
        return query.Where(x => x.Code.StartsWith(SearchCode, StringComparison.OrdinalIgnoreCase));
    }
    


    private IEnumerable<ProductInventoryViewModel>  CategoryFilter(IEnumerable<ProductInventoryViewModel> query)
    {

        if(CatFilter.SelectedCategory == SearchProductViewModel.DefaultValue)
            return query;
        
        return
        query.Where(x => string.Equals(x.Category.Name,CatFilter.SelectedCategory, StringComparison.OrdinalIgnoreCase));
        
    }

    private IEnumerable<ProductInventoryViewModel> LowStockFilter(IEnumerable<ProductInventoryViewModel> query)
    {
        if(!LowStockOnly)
            return query;
        return GetLowStock().Invoke(query);
    }

    private void UpdateGeneralInfo()
    {
        TotalInventory = DisplayedProducts.Sum(x => x.Total);
        TotalStock = DisplayedProducts.Sum(x => x.Stock);

    }
    private void UpdateUnfilteredInfo()
    {
        var lowStocks =  GetLowStock()(_inventoryState.Products);
        LowStockCount = lowStocks.Count();
        ProductsWithoutExistence = lowStocks.Count(x => x.Stock == 0);
    }

    private void ApplyFilters()
    {
        IEnumerable<ProductInventoryViewModel> query = Products;
        query = CategoryFilter(query);
        query = SearchbarFilter(query);
        query = LowStockFilter(query);

        DisplayedProducts.Clear();
        foreach(var item in query)
            DisplayedProducts.Add(item);

        UpdateGeneralInfo();
        
    }

    #endregion

}