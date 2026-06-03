using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jew.Avalonia.ViewModels.Products.Menus;
using Jew.Avalonia.Views.Products;

namespace Jew.Avalonia.ViewModels.SideBar;

public partial class SidebarViewModel(ProductsMenuViewModel productMenuVM) : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _currentViewModel;

    [RelayCommand]
    private void ShowProducts()
        => CurrentViewModel = productMenuVM;

}