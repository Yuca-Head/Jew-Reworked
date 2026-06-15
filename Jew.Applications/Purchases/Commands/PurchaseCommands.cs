
using Jew.Applications.InventoryMovements;
using Jew.Applications.InventoryMovements.Commands;
using Jew.Applications.ProductInventory.Queries;
using Jew.Applications.Purchases.Queries;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Exceptions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.Purchases.Commands;

public class PurchaseCommands
(IUnitOfWork context, MovementCommands movements, 
SupplierQueryService supplierQuery, ProductQueryService productQuery)
{

    private readonly MovementCommands _movements = movements;
    private readonly SupplierQueryService _supplierQuery = supplierQuery;
    private readonly ProductQueryService _productQuery = productQuery;
    private readonly IUnitOfWork _context = context;

    //Valida cosas generales de lista y que no pueda válidar por si solo el dominio (a veces redundancias útiles)
    public void RegisterPurchase(Purchase purchase)
    {
        //Para evitar problemas de milisegundos
        if(purchase.Date > DateTime.Now.AddMinutes(5))
            throw new PurchaseException("Ingrese una fecha válida de compra");
        if(!_supplierQuery.SupplierExists(purchase.SupplierId))
            throw new PurchaseException("Debe ingresar un proveedor para realizar la compra");
        if (!purchase.Items.Any())
            throw new PurchaseException("La compra debe tener al menos un producto");

        var addedMovements = new List<InventoryMovement>();
        
        //Lista para 
        foreach (var item in purchase.Items)
        {
            if(!_productQuery.ProductExists(item.ProductId))
                throw new PurchaseException($"Produto con código {item.ProductId} no encontrado", nameof(item.ProductId));


            
            var movement = new InventoryMovement(
                item.ProductId,
                item.Quantity,
                item.UnitCost,
                MovementType.In,
                purchase.TransactionId);
            addedMovements.Add(movement);
        }

        foreach(var movement in addedMovements)
            _movements.AddMovement(movement);

        _context.Purchases.Add(purchase);
        _context.SaveChanges();
    }

}