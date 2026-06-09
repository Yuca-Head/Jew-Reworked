using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Avalonia.ViewModels.Purchases.Inputs;
using Jew.Avalonia.ViewModels.Purchases.Outputs;

namespace Jew.Avalonia.ViewModels.Purchases.Menus;

public partial class PurchaseDetailsCardVM : ViewModelBase
{
    [ObservableProperty]
    private string? _details;
    

}