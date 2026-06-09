using Jew.Applications.InventoryMovements;
using Jew.Applications.InventoryMovements.Commands;
using Jew.Applications.ProductInventory;
using Jew.Applications.Purchases;
using Jew.Applications.Purchases.Commands;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Test.Application;

public class MovementsTests
{
    //Falta añadirle el repositorio a purchase service.
    [Fact]
    public void TransactionSavesProperly()
    {
        //Creation
        JsonUnitOfWork context = new(Jew.Infrastructure.Enums.Environment.Test);
        MovementCommands movementService = new(context);
        //PurchaseCommands service = new(movementService);
        
        context.Load();
        //Saving
        //service.RegisterPurchase(context.Suppliers.GetById(1), [new("Carne-Mol", 20, 15)]);
        context.SaveChanges();
    }
}