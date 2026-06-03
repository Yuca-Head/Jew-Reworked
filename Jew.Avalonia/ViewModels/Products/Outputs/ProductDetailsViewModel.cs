using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Avalonia.ViewModels.Categories.Outputs;


namespace Jew.Avalonia.ViewModels.Products.Outputs;

public partial class ProductDetailsViewModel : ProductViewModelBase
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActiveToString))]
    private bool _active;

    public string ActiveToString
    => Active ? "Activo" : "Inactivo";

    [ObservableProperty]
    private DateTime _createdDate;

    public ProductDetailsViewModel(ProductDto productDto, CategoryViewModel category):base(productDto, category)
    {
        Active = productDto.Active;
        CreatedDate = productDto.CreatedDate;
    }

    public ProductDetailsViewModel(ProductWithCategoryDto product) : this(product.ProductDto, 
    CategoryViewModel.From(product.CategoryDto))
    {}

    public ProductDetailsViewModel(ProductDetailsViewModel product)
    :this(new ProductDto(product.Code, product.Name, 
    product.Category!.Name, product.Active, product.CreatedDate), product.Category)
    {}
}