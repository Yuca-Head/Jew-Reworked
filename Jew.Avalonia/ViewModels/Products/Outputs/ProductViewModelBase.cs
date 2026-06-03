using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Avalonia.ViewModels.Categories.Outputs;
using Jew.Domain.ProductInventory.Entities;

namespace Jew.Avalonia.ViewModels.Products.Outputs;

public partial  class ProductViewModelBase : ViewModelBase
{
    [ObservableProperty]
    private string _code = "";
    [ObservableProperty]
    private string _name = "";

    [ObservableProperty]
    private CategoryViewModel? _category;    

    public ProductViewModelBase(ProductDto productDto, CategoryViewModel category)
    {
        Code = productDto.Code;
        Name = productDto.Name;
        Category = category;
    }

    public ProductViewModelBase(ProductWithCategoryDto dto):
    this(dto.ProductDto, CategoryViewModel.From(dto.CategoryDto))
    {
        
    }

    public ProductViewModelBase(ProductViewModelBase product)
    {
        this.Code = product.Code;
        this.Name = product.Name;
        this.Category = product.Category;
    }


}