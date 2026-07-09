using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.Sales.Commands;
using Jew.Applications.Sales.DTOs.Sales;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Sales.Transactions;
using Jew.Domain.Shared.Exceptions;

namespace Jew.Avalonia.ViewModels.Sales.Menus;

public partial class SaleSummaryCardVM
(SaleCommands saleCommands, Func<SaleDto?> createSale, Action clearForm) : ViewModelBase
{

    [ObservableProperty]
    private int _itemsCount;
    [ObservableProperty]
    private decimal _total;

    [ObservableProperty]
    private string _errorMessage = "";

    [ObservableProperty]
    private bool _showUnsafeSaleOption;

    private readonly Action _clearForm = clearForm;
    private readonly Func<SaleDto?> _createSale = createSale;
    private readonly SaleCommands _saleCommands = saleCommands;

    private void RegisterSale(Action<SaleDto> registerSale)
    {
        try
        {
            var sale = _createSale();
            if(sale is null)
                return;

            ShowUnsafeSaleOption = false;
            
            registerSale.Invoke(sale);
            WeakReferenceMessenger.Default.Send(new StockStateUpdatedMessage([..sale.Items.Select(x => x.ProductId)]));
            WeakReferenceMessenger.Default.Send(new MovementUpdateMessage(null, sale.TransactionId));
            ClearForm();    
        }catch(DomainException e)
        {
            ErrorMessage  = e.Message;  
            if(string.Equals(e.FieldName, nameof(SaleItemDto.UnitPrice), StringComparison.OrdinalIgnoreCase))
                ShowUnsafeSaleOption = true;
            
        }
    }

    [RelayCommand]
    private void RegisterSafeSale()
    => RegisterSale(async x => await _saleCommands.RegisterSafeSale(x));
    [RelayCommand]
    private void RegisterUnsafeSale()
    => RegisterSale(async x => await _saleCommands.RegisterUnsafeSale(x));

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
    => _clearForm.Invoke();


}