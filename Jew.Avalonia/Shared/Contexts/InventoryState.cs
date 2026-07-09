using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        _ = Update(null);
        WeakReferenceMessenger.Default.Register<StockStateUpdatedMessage>(this,
        async (_, message) => await Update(message.ProductsChangedCode));

        
        WeakReferenceMessenger.Default.Register<ProductUpdateMessage>(this,
        async (_,e) =>
        {
            if(e.Action is ProductUpdateMessage.Activator.Modified)
                await Update(e.ProductsChangedCode);
        });

    }

    private readonly InventoryQueryService _queryService;
    public IReadOnlyCollection<string>? CodesChanged {get; private set;}
    public event EventHandler? StateChanged;
    public ObservableCollection<ProductInventoryViewModel> Products {get;} = [];

   
    private async Task Update(IEnumerable<string>? args)
    {
        if(args is null)
            CodesChanged = null;
        else
            CodesChanged = [..args];
        if(args is null)    
        {
            Products.Clear();
            foreach(var p in (await _queryService.GetAvailableProducts()).Select(x => new ProductInventoryViewModel(x)))
                Products.Add(p);
        }
        else
        {
            foreach(var code in args)
            {
                var item = await _queryService.GetProductInventory(code);

                var vm = Products.FirstOrDefault(x => x.Code == code);
                
                if(vm is null)
                    Products.Add(new(item));
                else
                    if(item.Product.ProductDto.Active)
                    {
                        vm.Stock = item.Stock;
                        vm.AverageCost = item.Cost;
                    }
                    else
                        Products.Remove(vm);
                   
            }
        }

        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}