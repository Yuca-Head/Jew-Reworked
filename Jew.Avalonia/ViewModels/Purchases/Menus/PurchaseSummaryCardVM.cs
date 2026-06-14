using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.Purchases.Commands;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Avalonia.ViewModels.Purchases.Outputs;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Exceptions;

namespace Jew.Avalonia.ViewModels.Purchases.Menus;

public partial class PurchaseSummaryCardVM(PurchaseCommands purchaseCommands, Func<Purchase> createPurchase, Action clearForm) : ViewModelBase
{
    private readonly PurchaseCommands _purchaseCommands = purchaseCommands;

    [ObservableProperty]
    private int _itemsCount;
    [ObservableProperty]
    private decimal _total;

    [ObservableProperty]
    private string _errorMessage = "";

    private readonly Action _clearForm = clearForm;

    private readonly Func<Purchase> _createPurchase = createPurchase;

    [RelayCommand]
    private void RegisterPurchase()
    {
        //Aquí trato de registrar una venta
        try
        {
            var purchase = _createPurchase();
            _purchaseCommands.RegisterPurchase(purchase);
            WeakReferenceMessenger.Default.Send(new StockStateUpdatedMessage(purchase.Items.Select(x => x.ProductId)));
            ClearForm();
        }catch(DomainException e)
        {
            ErrorMessage  = e.Message;  

        }
        //error completamente innesperado que no he podido resolver por eso está, pero lo dejo por falta de tiempo
        catch(NullReferenceException e)
        {
            System.Console.WriteLine("Error inevitable de vez en cuando: " + e.Message);
            ErrorMessage = "Debe rellenar el formulario para realizar una compra";
        }
    }

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    partial void OnErrorMessageChanged(string value)
    {
        OnPropertyChanged(nameof(HasError));
    }

    internal void UpdateDisplayedValues(IEnumerable<AddItemCartVM> cartProducts)
    {
        Total = cartProducts.Sum(p => p.SubTotal);
        ItemsCount = cartProducts.Sum(p => p.Quantity);
    }

    [RelayCommand]
    private void ClearForm()
    => _clearForm?.Invoke();


}