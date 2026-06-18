using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.InventoryMovements.Queries;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Queries;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.ViewModels.Products.Outputs;
using Jew.Domain.InventoryMovements.Entities;

namespace Jew.Avalonia.Shared.Contexts;

public sealed class InventoryState
{
    public InventoryState(InventoryQueryService queryService) 
    {
        _queryService = queryService;
        Update(null);
        WeakReferenceMessenger.Default.Register<StockStateUpdatedMessage>(this,
        (_, message) => Update(message.ProductsChangedCode));
    }

    private readonly InventoryQueryService _queryService;
    public IReadOnlyCollection<string>? CodesChanged {get; private set;}
    public event EventHandler? StateChanged;
    public ObservableCollection<ProductInventoryViewModel> Products {get;} = [];

   
    private void Update(IEnumerable<string>? args)
    {
        if(args is null)
            CodesChanged = null;
        else
            CodesChanged = [..args];
        if(args is null)
        {
            Products.Clear();
            foreach(var p in _queryService.GetPurchasedProductsOnly().Select(x => new ProductInventoryViewModel(x)))
                Products.Add(p);
        }
        else
        {
            foreach(var code in args)
            {
                var item = _queryService.GetProductInventory(code);

                var vm = Products.FirstOrDefault(x => x.Code == code);

                if(vm is null)
                    Products.Add(new(item));
                else
                {
                    vm.Stock = item.Stock;
                    vm.AverageCost = item.Cost;
                }   
            }
        }

        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}