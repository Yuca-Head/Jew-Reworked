using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.InventoryMovements.Queries;
using Jew.Applications.ProductInventory.Queries;
using Jew.Applications.Sales.DTOs.Sales;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Products.Outputs;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Avalonia.ViewModels.Transactions;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Sales.Transactions;

namespace Jew.Avalonia.ViewModels.Sales.Menus;

public partial class SaleCartCardVM : CartProductsVM
{
    public SaleCartCardVM(InventoryState inventoryState)
    {
        _inventoryState = inventoryState;


        _inventoryState.StateChanged += (_,_) => UpdateAllowedProducts();
        UpdateAllowedProducts();
    }

    
    private InventoryState _inventoryState;
    private readonly Dictionary<string,ProductInventoryViewModel> _allowedProducts = [];

    private void UpdateAllowedProducts()
    {
        var changedCodes = _inventoryState.CodesChanged;
        if (changedCodes is null)
        {
            _allowedProducts.Clear();

            foreach (var p in _inventoryState.Products.Where(x => x.Stock > 0))
            {
                _allowedProducts[p.Code] = p;
            }

            return;
        }

        foreach (var code in changedCodes)
        {
            var product = _inventoryState.Products
                .FirstOrDefault(p => p.Code == code);

            if (product is null || product.Stock <= 0)  
                _allowedProducts.Remove(code);
            else
                _allowedProducts[code] = product;
            
        }
    }


    protected override void RemoveItem(AddItemCartVM item)
    {
        Cart.Remove(item);
        UpdateItemsNumber();
    }
    
    [RelayCommand]
    private void AddItemToCart(string code)
    {
        var item = _allowedProducts.Select(x => new AddItemCartVM(x.Value)).FirstOrDefault(x => x.Code == code)!;
        item.ItemNumber = Cart.Count + 1;
        Cart.Add(item);
    }

    private void UpdateItemsNumber()
    {
        int count = 0;
        foreach(var item in Cart)
            item.ItemNumber = ++count; 
    }

    public void Clear()
    {
        Cart.Clear();
        UpdateItemsNumber();
    }

    #region Busqueda
    [ObservableProperty]
    private string _searchCode = "";
    public ObservableCollection<ProductViewModelBase> Suggestions {get; private set;} = [];

    partial void OnSearchCodeChanged(string value)
    {
        Suggestions.Clear();

        var selectedKeys = Cart
        .Select(x => x.Code)
        .ToHashSet();
        
        if(string.IsNullOrEmpty(value))
        {
            FilterSuggestions(x => !selectedKeys.Contains(x.Key));
            return;
        }


        FilterSuggestions(x => !selectedKeys.Contains(x.Key) && (x.Key.StartsWith(value, StringComparison.OrdinalIgnoreCase) ||
                x.Value.Name.Contains(value, StringComparison.OrdinalIgnoreCase)));
    }
    

    private void FilterSuggestions(Func<KeyValuePair<string, ProductInventoryViewModel>, bool> selector)
    {
        foreach(var product in _allowedProducts
        .Where
        (selector)
        .Take(10)
        )
            Suggestions.Add
            (new ProductViewModelBase(new(product.Key, product.Value.Name, 
            product.Value.Category.Name, product.Value.Active, product.Value.CreatedDate), product.Value.Category));
    
    }

    [RelayCommand]
    private void SelectProduct(string code)
    {
        AddItemToCart(code);
        OnSearchCodeChanged("");
    }

    #endregion
}