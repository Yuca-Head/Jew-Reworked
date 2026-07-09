using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Domain.Shared.Keys;

namespace Jew.Avalonia.ViewModels.Suppliers.Outputs;

public partial class SupplierViewModel(CodeKey id, string name) : ViewModelBase
{
    [ObservableProperty]
    private CodeKey _id = id;
    [ObservableProperty]
    private string _name = name;

    public static SupplierViewModel From(SupplierDto supplierDto)
    => new(supplierDto.CodeKey, supplierDto.Name);
}