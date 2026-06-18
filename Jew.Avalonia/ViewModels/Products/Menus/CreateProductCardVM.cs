using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.ProductInventory.Commands;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Products.Inputs;
using Jew.Avalonia.ViewModels.Products.Services;
using Jew.Domain.Shared.Exceptions;

namespace Jew.Avalonia.ViewModels.Products.Menus;

public partial class CreateProductCardVM : ViewModelBase
{

    public CreateProductCardVM(ProductState productState, InventoryCommands commands)
    {
        _commands = commands;
        ProductState = productState;
    }   

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    private readonly InventoryCommands _commands;

    public ProductState ProductState {get;}


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string? _errorMessage;
    
    [ObservableProperty]
    private SearchProductViewModel _categoryFilter;

    [ObservableProperty]
    private CreateProductViewModel _product = new();
    
    [RelayCommand]
    private void CreateProduct()
    {
        try
        {
            _commands.AddProduct(new(Product.Code, Product.Name, Product.Category));
            WeakReferenceMessenger.Default.Send(new ProductUpdateMessage(Product.Code){ Action = ProductUpdateMessage.Activator.Added});
            Clear();
        }
        catch(DomainException e)
        {
            ErrorMessage = e.Message;
        }

    }

    [RelayCommand]
    private void Clear()
    {
        CategoryFilter.SelectedCategory = "Escoger Categoría";
        Product = new();    
        ErrorMessage = "";
    }
}