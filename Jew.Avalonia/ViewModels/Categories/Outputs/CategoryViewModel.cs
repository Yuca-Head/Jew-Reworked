using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Domain.ProductInventory.Entities;

namespace Jew.Avalonia.ViewModels.Categories.Outputs;

public sealed partial class CategoryViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _description;

    public CategoryViewModel(string name, string description)
    {
        Name = name;
        Description = description;
    }   

    public static CategoryViewModel From(CategoryDto categoryDto)
    => new(categoryDto.Name, categoryDto.Description);
}