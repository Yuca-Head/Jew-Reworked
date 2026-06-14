using System;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Purchases.Commands;
using Jew.Applications.Purchases.Queries;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Avalonia.ViewModels.Transactions;
using Jew.Domain.Purchases.Transactions;

namespace Jew.Avalonia.ViewModels.Purchases.Menus;

public partial class PurchasesMenuViewModel : ViewModelBase
{
    public PurchasesMenuViewModel(SupplierQueryService supplierQuery, PurchaseCommands purchaseCommands)
    {
        _supplierQuery = supplierQuery;
        SupplierCardVM = new(_supplierQuery);

        Cart = new(_supplierQuery, null!);
        Details = new();
        _summary = new(purchaseCommands, CreatePurchase(), ClearForm);
        SupplierCardVM.PropertyChanged += SupplierCard_PropertyChanged;

        Cart.Cart.CollectionChanged += (_, e) =>
        {
            if (e.NewItems is not null)
            {
                foreach (AddItemCartVM item in e.NewItems)
                {
                    item.PropertyChanged += (_, _) =>
                        Summary.UpdateDisplayedValues(Cart.Cart);
                }
            }

            Summary.UpdateDisplayedValues(Cart.Cart);
        };
    }

    private readonly SupplierQueryService _supplierQuery;

    [ObservableProperty]    
    private PurchaseSupplierCardVM _supplierCardVM;
    [ObservableProperty]
    private PurchaseProductsCartVM _cart;
    [ObservableProperty]
    private TransactionDetailsCardVM _details;
    [ObservableProperty]
    private PurchaseSummaryCardVM _summary;


    private void SupplierCard_PropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName ==
            nameof(PurchaseSupplierCardVM.SupplierSelected))
        {
            Cart.Supplier = SupplierCardVM.SupplierSelected;
            Cart.Cart.Clear();
        }
    }

    private Func<Purchase> CreatePurchase()
    //Esta línea tira la excepción excepcional
    => () => new(SupplierCardVM.SupplierSelected.Id, 
        [..Cart.Cart.Select(x => new PurchaseItem(x.Code, x.Quantity, x.UnitCost))],
        SupplierCardVM.TransactionId, Details.Details, SupplierCardVM.OrderDate!.Value.DateTime);
    
    
    public void ClearForm()
    {
        SupplierCardVM.Clear();
        Cart.Clear();
        Details.Details = "";
        Summary.ErrorMessage = "";
    }
}
