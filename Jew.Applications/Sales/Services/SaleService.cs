
using Jew.Applications.InventoryMovements;
using Jew.Applications.InventoryMovements.Commands;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Sales.Entities;
using Jew.Domain.Sales.Exceptions;
using Jew.Domain.Sales.Transactions;

namespace Jew.Applications.Sales.Services;

public class SaleSevices(MovementCommands movements)
{
    private readonly MovementCommands _movements = movements;

    public void RegisterSale(Client client, Sale sale)
    {
        if(sale.Items.Count <= 0)
            throw new SaleException("Se debe tener al menos un item para realizar la venta.", nameof(sale));

        var transactionId = Guid.NewGuid();
        //_salesRepo.Add(sale); // guardar factura

        foreach (var item in sale.Items)
        {

            _movements.AddMovement(new InventoryMovement(
                item.ProductId,
                item.Quantity,
                item.UnitPrice,
                MovementType.Out,
                transactionId 
            )
            );

        }
    }
}