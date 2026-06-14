using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jew.Avalonia.ViewModels.Purchases.Inputs;

namespace Jew.Avalonia.ViewModels.Transactions;

public abstract partial class CartProductsVM : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<AddItemCartVM> _cart = [];
    [RelayCommand]
    protected abstract void RemoveItem(AddItemCartVM item);
}