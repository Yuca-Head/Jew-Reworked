using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
    private SupplierViewModel _supplierSelected = DefaultSupplier;

    [ObservableProperty]
    private DateTimeOffset? _orderDate = DateTimeOffset.Now;

    [ObservableProperty]
    private Guid _transactionId = Guid.NewGuid();

    public ObservableCollection<SupplierProductDto> SupplierProducts{get; private set;} = [];

    public PurchaseSupplierCardVM(SupplierQueryService supplierQuery)
    {   
        _supplierQuery = supplierQuery;
        InitializeSuppliers();
    }

    private async void InitializeSuppliers()
    { 
        foreach(var supp in (await _supplierQuery.GetSuppliers()).Select(x => new SupplierViewModel(x.CodeKey, x.Name)))
            Suppliers.Add(supp);
    }


    async partial void OnSupplierSelectedChanged(SupplierViewModel? oldValue, SupplierViewModel newValue)
    {
        if(oldValue != newValue && newValue != DefaultSupplier && SupplierSelected is not null)
            SupplierProducts = [..await _supplierQuery.GetProductsFromSupplier(SupplierSelected.Id)];
    }

    public void Clear()
    {
        SupplierSelected = DefaultSupplier;
        TransactionId = Guid.NewGuid();
        SupplierProducts.Clear();   
    }

    public static SupplierViewModel DefaultSupplier {get;} = new(new(5,"-----"), "Escoger Proveedor");
}