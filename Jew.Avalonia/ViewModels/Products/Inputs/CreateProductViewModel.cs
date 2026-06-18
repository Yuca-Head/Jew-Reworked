using CommunityToolkit.Mvvm.ComponentModel;

namespace Jew.Avalonia.ViewModels.Products.Inputs;

public partial class CreateProductViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _code;
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _category;
}