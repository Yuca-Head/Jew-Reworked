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
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActiveToString))]
    private bool _active;

    public string ActiveToString
    => Active ? "Activo" : "Inactivo";

    [ObservableProperty]
    private DateTime _createdDate;

    public ProductViewModelBase(ProductDto productDto, CategoryViewModel category)
    {
        Code = productDto.Code;
        Name = productDto.Name;
        Category = category;
        Active = productDto.Active;
        CreatedDate = productDto.CreatedDate;
    }

    public ProductViewModelBase(ProductWithCategoryDto dto):
    this(dto.ProductDto, CategoryViewModel.From(dto.CategoryDto))
    {}

    public ProductViewModelBase(ProductViewModelBase product)
    {
        this.Code = product.Code;
        this.Name = product.Name;
        this.Category = product.Category;
    }


}