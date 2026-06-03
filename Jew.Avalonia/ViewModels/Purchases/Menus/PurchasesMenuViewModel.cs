using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Jew.Applications.Purchases.Commands;
using Jew.Applications.Purchases.Queries;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Domain.Purchases.Entities;

namespace Jew.Avalonia.ViewModels.Purchases.Menus;

public partial class PurchasesMenuViewModel : ViewModelBase
{
    private readonly SupplierQueryService _supplierQuery;
    public ObservableCollection<AddProductPurchaseVM> CarProducts;
    public PurchasesMenuViewModel(SupplierQueryService supplierQuery)
    {
        _supplierQuery = supplierQuery;
        
    }   
}