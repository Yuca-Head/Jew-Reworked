using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Domain.Purchases.Transactions;

namespace Jew.Avalonia.ViewModels.Purchases.Outputs;

public partial class RegisterPurchaseViewModel : ViewModelBase
{
    [ObservableProperty]
    private decimal _total;
    [ObservableProperty]
    private int _itemsCount;    

    public void UpdateDisplayedValues(IEnumerable<AddItemCartVM> products)
    {
        Total = products.Sum(x => x.SubTotal);
        ItemsCount = products.Sum(x => x.Quantity);
    }
}

