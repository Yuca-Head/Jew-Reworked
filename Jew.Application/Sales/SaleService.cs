
using Jew.Application.InventoryMovements;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Sales.Exceptions;
using Jew.Domain.Sales.Transactions;

namespace Jew.Application.Sales;

public class SaleSevices(MovementService movements)
{
    private readonly MovementService _movements = movements;

    public void RegisterSale(Sale sale)
    {
        if(sale.Items.Count <= 0)
            throw new SaleException("Se debe tener al menos un item para realizar la venta.", nameof(sale));

        var transactionId = Guid.NewGuid();
        //_salesRepo.Add(sale); // guardar factura

        foreach (var item in sale.Items)
        {

            _movements.AddMovement(new InventoryMovement(
                item.Product.Code,
                item.Quantity,
                item.UnitPrice,
                MovementType.Out,
                sale.Client,
                transactionId 
            )
            );

        }
    }
}