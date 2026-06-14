using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jew.Avalonia.ViewModels.Inventory.Menus;
using Jew.Avalonia.ViewModels.Products.Menus;
using Jew.Avalonia.ViewModels.Purchases.Menus;
using Jew.Avalonia.ViewModels.Sales.Menus;
using Jew.Avalonia.Views.Products;

namespace Jew.Avalonia.ViewModels.SideBar;

public partial class SidebarViewModel
(
    ProductsMenuViewModel productMenuVM, PurchasesMenuViewModel purchasesMenu, InventoryMenuViewModel inventoryMenu,
    SalesMenuViewModel salesMenuVM
) : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _currentViewModel;
    
    
    [RelayCommand]
    private void ShowProducts()
    {
        if(CurrentViewModel != productMenuVM)
            CurrentViewModel = productMenuVM;
    }
    
    [RelayCommand]
    private void ShowPurchases()
    {
        if(CurrentViewModel == purchasesMenu)
            return;
        CurrentViewModel = purchasesMenu;
    }

    [RelayCommand]
    private void ShowInventory()
    {
        if(CurrentViewModel != inventoryMenu)
            CurrentViewModel = inventoryMenu;
    }

    [RelayCommand]
    private void ShowSales()
    {
        if(CurrentViewModel != salesMenuVM)
            CurrentViewModel = salesMenuVM;
    }

}