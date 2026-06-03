using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.Commands;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Queries;
using Jew.Avalonia.ViewModels.Categories.Outputs;
using Jew.Avalonia.ViewModels.Products.Outputs;
using Jew.Avalonia.ViewModels.Products.Services;
using Jew.Domain.ProductInventory.Entities;

namespace Jew.Avalonia.ViewModels.Products.Menus;

public sealed partial class ProductsMenuViewModel(SearchProductViewModel searchProductViewModel) : ViewModelBase
{
    [ObservableProperty]
    private SearchProductViewModel _searchProduct = searchProductViewModel;
}