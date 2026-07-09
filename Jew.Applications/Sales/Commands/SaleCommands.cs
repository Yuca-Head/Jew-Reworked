
using System.Threading.Tasks;
using Jew.Applications.InventoryMovements;
using Jew.Applications.InventoryMovements.Commands;
using Jew.Applications.InventoryMovements.Queries;
using Jew.Applications.ProductInventory.Queries;
using Jew.Applications.Sales.DTOs.Sales;
using Jew.Applications.Sales.Queries;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Sales.Entities;
using Jew.Domain.Sales.Exceptions;
using Jew.Domain.Sales.Transactions;

namespace Jew.Applications.Sales.Commands;

public sealed class SaleCommands
(IUnitOfWork context, MovementCommands movements, ProductQueryService productQuery, ClientQueryService clientQuery, StockStateQueryService stockState)
{
    private readonly MovementCommands _movements = movements;
    private readonly IUnitOfWork _context = context;
    private readonly ProductQueryService _productQuery = productQuery;
    private readonly ClientQueryService _clientQuery = clientQuery;
    private readonly StockStateQueryService _stockSateQuery = stockState;

    public Task RegisterSafeSale(SaleDto sale)
    => RegisterSale(sale, unsafeSale: false);

    public Task RegisterUnsafeSale(SaleDto sale)
    => RegisterSale(sale, unsafeSale: true);

    private async Task RegisterSale(SaleDto sale, bool unsafeSale)
    {
        if(!sale.Items.Any())
            throw new SaleException("Se debe tener al menos un item para realizar la venta.", nameof(sale));
        //Para evitar problemas de milisegundos (igual que compras)
        if(sale.Date > DateTime.Now.AddMinutes(5))
            throw new SaleException("Ingrese una fecha válida de venta");
        if(!await _clientQuery.ClientExists(sale.ClientKey.Key))
            throw new SaleException("Debe ingresar un cliente existente para realizar la compra");

        var addedMovements = new List<InventoryMovement>();

        foreach (SaleItemDto item in sale.Items)
        {
                        
            if(!await _productQuery.ProductExists(item.ProductId))
                throw new SaleException($"Produto con código {item.ProductId} no encontrado", nameof(item.ProductId));
            if(item.Quantity <= 0)
                throw new SaleException($"Debe ingresar al menos un producto {item.ProductId} para realizar la venta");
            if(item.UnitPrice <= 0)
                throw new SaleException($"El precio del producto {item.ProductId} debe ser mayor a 0");

            var state = await _stockSateQuery.GetProductState(item.ProductId);

            if(state!.Quantity < item.Quantity)
                throw new SaleException($"No puede vender una cantidad de {item.Quantity} "+
                $"del producto {item.ProductId} cuando solo contiene {state.Quantity} existencias");
            
            if(!unsafeSale && state.AverageCost * 1.2m > item.UnitPrice)
                throw new SaleException
                ($"El valor del producto {item.ProductId} es muy bajo (su valor unitario es de {state.AverageCost:C}). "+
                $"Precio minimo sugerido: {state.AverageCost * 1.2m:C}", nameof(SaleItemDto.UnitPrice));

            var movement = new InventoryMovement(
                item.ProductId,
                item.Quantity,
                state.AverageCost,
                MovementType.Out,
                sale.TransactionId);
            addedMovements.Add(movement);
        }

       
        await _movements.AddMovements(addedMovements);
        await _context.Sales.AddAsync(SaleDto.To(sale));
        await _context.SaveChangesAsync();
    }
}