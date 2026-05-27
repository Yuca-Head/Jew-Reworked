
using Jew.Applications.InventoryMovements;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Purchases.Transactions;

namespace Jew.Applications.Purchases;

public class PurchaseService(MovementService movements)
{

    private readonly MovementService _movements = movements;

    public void RegisterPurchase(Supplier supplier, List<PurchaseItem> items)
    {
        ArgumentNullException.ThrowIfNull(supplier);
        ArgumentNullException.ThrowIfNull(items);
        if (items.Count == 0)
            throw new PurchaseException("La compra debe tener al menos un producto");

        var transactionId = Guid.NewGuid();

        var purchase = new Purchase(supplier.Key, items, transactionId);

        foreach (var item in purchase.Items)
        {
        
            _movements.AddMovement(new InventoryMovement(
                item.ProductId,
                item.Quantity,
                item.UnitCost,
                MovementType.In,
                supplier,
                transactionId
            ));
        }
    }

}