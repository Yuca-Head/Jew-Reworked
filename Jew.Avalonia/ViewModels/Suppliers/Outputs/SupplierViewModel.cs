using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Purchases.DTOs.Suppliers;

namespace Jew.Avalonia.ViewModels.Suppliers.Outputs;

public partial class SupplierViewModel(int id, string name) : ViewModelBase
{
    [ObservableProperty]
    private int _id = id;
    [ObservableProperty]
    private string _name = name;

    public static SupplierViewModel From(SupplierDto supplierDto)
    => new(supplierDto.Id, supplierDto.Name);
}