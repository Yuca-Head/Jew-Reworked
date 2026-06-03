using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Avalonia.ViewModels.Categories.Outputs;

namespace Jew.Avalonia.ViewModels.Products.Menus;

public partial class CreateProductViewModel(IReadOnlyCollection<string> categories) : ViewModelBase
{
    [ObservableProperty]
    private  IReadOnlyCollection<string> _categories = categories;
    
    [ObservableProperty]
    private string _selectedCategory = string.Empty;
}