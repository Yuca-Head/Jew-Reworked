using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
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
        _productState = productState;
        _productState.CategoriesChanged += (_,_) => UpdateCategories();
        Categories = [];
        UpdateCategories();
    }   

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    private readonly InventoryCommands _commands;

    private readonly ProductState _productState;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string? _errorMessage;
    

    [ObservableProperty]
    private CreateProductViewModel _product = new();
    
    public ObservableCollection<string> Categories {get;} = [];
        
    [RelayCommand]
    private async Task CreateProduct()
    {
        try
        {
            await _commands.AddProduct(new(Product.Code, Product.Name, Product.Category));
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
        Product = new();    
        ErrorMessage = "";
    }

    private void UpdateCategories()
    {
        Categories.Clear();
        foreach(var c in _productState.Categories.Select(x => x.Name))
            Categories.Add(c);
    }
}