using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.ProductInventory.Commands;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.ViewModels.Categories.Outputs;
using Jew.Domain.Shared.Exceptions;

namespace Jew.Avalonia.ViewModels.Products.Menus;

public partial class CreateCategoryCardVM(InventoryCommands commands) : ViewModelBase
{

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string? _errorMessage;
    [ObservableProperty]
    private CategoryViewModel _category = new("","");


    [RelayCommand]
    private void CreateCategory()
    {
        try
        {
            commands.AddCategory(new(Category.Name, Category.Description));
            WeakReferenceMessenger.Default.Send(new CategoryUpdateMessage(Category.Name));
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
        Category = new("","");
        ErrorMessage = "";
    }
}