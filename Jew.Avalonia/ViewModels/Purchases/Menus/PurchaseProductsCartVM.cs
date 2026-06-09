using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jew.Applications.Purchases.Queries;
using Jew.Avalonia.ViewModels.Products.Outputs;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Avalonia.ViewModels.Suppliers.Outputs;
using Jew.Domain.Purchases.Entities;

namespace Jew.Avalonia.ViewModels.Purchases.Menus;

public partial class PurchaseProductsCartVM : ViewModelBase
{
    public PurchaseProductsCartVM(SupplierQueryService supplierQuery, SupplierViewModel supplier)
    {
        _supplierQuery = supplierQuery;

        Supplier = supplier;
    }

    private readonly SupplierQueryService _supplierQuery;
    [ObservableProperty]
    private SupplierViewModel _supplier;
    [ObservableProperty]
    private ObservableCollection<SupplierProductViewModel> _supplierProducts  = [];
    public List<SupplierProductViewModel> _allProducts = [];
    [ObservableProperty]
    private ObservableCollection<AddProductPurchaseVM> _cartProducts = [];

    //Busqueda
    [ObservableProperty]
    private string _searchCode = "";
    public ObservableCollection<ProductViewModelBase> Suggestions {get; private set;} = [];

    partial void OnSupplierChanged(SupplierViewModel? oldValue, SupplierViewModel newValue)
    {
        
        if(oldValue == newValue || newValue.Id < 0)
            return;

        _allProducts.Clear();
        foreach(var item in _supplierQuery.GetSupplierProducts(Supplier.Id))
            _allProducts.Add(SupplierProductViewModel.From(item));
        
        UpdateSupplierProducts();
    }


    private void UpdateSupplierProducts()
    {
        SupplierProducts.Clear();
        var selectedKeys = CartProducts
        .Select(x => x.Code)
        .ToHashSet();

        SupplierProducts = [.._allProducts
            .Where(p => !selectedKeys.Contains(p.Product.Code))];
    }

    
    partial void OnSearchCodeChanged(string value)
    {
        Suggestions.Clear();

        if(string.IsNullOrWhiteSpace(value))
            return;

        foreach(var product in SupplierProducts
            .Where(x => x.Product.Code.StartsWith(value, StringComparison.OrdinalIgnoreCase))
            .Take(10)
            .Select(p => p.Product)
            )
        {
            Suggestions.Add(new(product));
        }
    }

    private void UpdateItemsNumber()
    {
        int count = 0;
        foreach(var item in CartProducts)
            item.ItemNumber = ++count; 
            
    }

    [RelayCommand]
    private void SelectProduct(ProductViewModelBase product)
    {
        AddItemToCart(product);

        UpdateSupplierProducts();

        SearchCode = "";
        Suggestions.Clear();
    }



    private void AddItemToCart(ProductViewModelBase product)
    => CartProducts.Add(new(product){ItemNumber = CartProducts.Count + 1});
    

    [RelayCommand]
    private void RemoveItem(AddProductPurchaseVM item)
    {
        CartProducts.Remove(item);
        UpdateItemsNumber();
        UpdateSupplierProducts();
    }

    public void Clear()
    {
        CartProducts.Clear();
        _allProducts.Clear();
        UpdateSupplierProducts();
        UpdateItemsNumber();
    }

}