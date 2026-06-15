using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Sales.Commands;
using Jew.Applications.Sales.DTOs.Sales;
using Jew.Applications.Sales.Queries;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Avalonia.ViewModels.Transactions;

namespace Jew.Avalonia.ViewModels.Sales.Menus;

    public partial class SalesMenuViewModel : ViewModelBase
{
    
    public SalesMenuViewModel(ClientQueryService clientQuery, InventoryState inventoryState, SaleCommands commands)
    {
        ClientCardVM = new(clientQuery);
        Cart = new(inventoryState);
        Details = new();
        Summary = new(commands, CreateSale(), ClearForm);
        
        Cart.Cart.CollectionChanged += (_, e) =>
        {
            if (e.NewItems is not null)
                foreach (AddItemCartVM item in e.NewItems)
                    item.PropertyChanged += (_, _) =>
                        Summary.UpdateDisplayedValues(Cart.Cart);
                
            Summary.UpdateDisplayedValues(Cart.Cart);
        };
    }   

    [ObservableProperty]
    private SaleClientCardVM _clientCardVM;
    [ObservableProperty]
    private SaleCartCardVM _cart;
    [ObservableProperty]
    private TransactionDetailsCardVM _details;
    [ObservableProperty]
    private SaleSummaryCardVM _summary;

    private Func<SaleDto?> CreateSale()
    => () => 
        {   
            if(Cart.Cart.Count == 0)
            {
                Summary.ErrorMessage = "Debe rellenar el formulario para realizar la venta";
                return null;
            }

            return new(ClientCardVM.ClientSelected.CodeKey, 
            [..Cart.Cart.Select(x => new SaleItemDto(x.Code, x.UnitCost, x.Quantity))],
            ClientCardVM.TransactionId, Details.Details, ClientCardVM.OrderDate!.Value.DateTime);
        };
    
    
    public void ClearForm()
    {
        ClientCardVM.Clear();
        Cart.Clear();
        Details.Details = "";
        Summary.ErrorMessage = "";
        Summary.ShowUnsafeSaleOption = false;
    }
}