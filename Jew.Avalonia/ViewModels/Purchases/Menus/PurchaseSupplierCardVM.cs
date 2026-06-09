using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Applications.Purchases.Queries;
using Jew.Avalonia.ViewModels.Suppliers.Outputs;
using Jew.Domain.Purchases.Entities;

namespace Jew.Avalonia.ViewModels.Purchases.Menus;

public partial class PurchaseSupplierCardVM: ViewModelBase
{
    private readonly SupplierQueryService _supplierQuery;
    public ObservableCollection<SupplierViewModel> Suppliers {get; set;} = [];

    [ObservableProperty]
    private SupplierViewModel _supplierSelected = new(-1, "Escoger Proveedor");

    [ObservableProperty]
    private DateTimeOffset? _orderDate = DateTimeOffset.Now;

    [ObservableProperty]
    private Guid _transactionId = Guid.NewGuid();

    public ObservableCollection<SupplierProductDto> SupplierProducts{get; private set;} = [];

    public PurchaseSupplierCardVM(SupplierQueryService supplierQuery)
    {   
        _supplierQuery = supplierQuery;
        Suppliers = [.._supplierQuery.GetSuppliers().Select(x => new SupplierViewModel(x.Key, x.Name))];
    }

    partial void OnSupplierSelectedChanged(SupplierViewModel? oldValue, SupplierViewModel newValue)
    {
        if(oldValue != newValue && newValue.Id > 0)
            SupplierProducts = [.._supplierQuery.GetSupplierProducts(SupplierSelected.Id)];
    }

    public void Clear()
    {
        SupplierSelected = new(-1, "Escoger Proveedor");
        TransactionId = Guid.NewGuid();
        SupplierProducts.Clear();   
    }
}