using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.ProductInventory.Commands;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.ViewModels.Products.Outputs;
using Jew.Domain.Shared.Exceptions;

namespace Jew.Avalonia.ViewModels.Products.Inputs;

public partial class ModifyProductViewModel : ViewModelBase
{
    public ModifyProductViewModel(ModifyProductsCommands commands)
    {
        _commands = commands;
        Product = new(new ProductDto("","","",false, default), new("---", ""));
    }

    
    private readonly ModifyProductsCommands _commands;   

    [ObservableProperty]
    private ProductViewModelBase? _product;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string _errorMessage = "";

    public bool HasError => ErrorMessage.Length != 0;

    [ObservableProperty]
    private string _name = "";
    [ObservableProperty]
    private bool _state = true;

    partial void OnProductChanged(ProductViewModelBase? oldValue, ProductViewModelBase? newValue)
    {
        ErrorMessage = "";
        if(Product is null)
            return;
        Name = Product.Name;
        State = Product.Active;
    }

    

    [RelayCommand]
    private async Task ModifyProduct()
    {
        if(Product is null)
        {
            Clear();
            return;
        }
        if(Product.Name == Name && Product.Active == State)
            return;
        try
        {
            await _commands.ModifyProduct(new(Product.Code, Name, State));
            WeakReferenceMessenger.Default.Send<ProductUpdateMessage>
            (new(Product.Code){Action = ProductUpdateMessage.Activator.Modified});
            Clear();
        }
        catch(DomainException e)
        {
            ErrorMessage = e.Message;
        }
    }

    private void Clear()
    {
        Product = null;
        Name = "";
        State = false;
        ErrorMessage = "";
    }
}