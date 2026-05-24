using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Domain.Purchases.Repositories;
using Jew.Domain.Sales.Repositories;

namespace Jew.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    ICategoriesRepo Categories { get; }
    IProductsRepo Products { get; }
    ISuppliersRepo Suppliers { get; }
    IMovementsRepo Movements { get; }
    IStockStateRepo StockState { get; }
    ISaleRepo Sales { get; }
    IPurchaseRepo Purchases { get; }

    //No sé que hace pero cuando lo sepa lo usaré.
    virtual void Commit()
    => throw new NotImplementedException();
    void SaveChanges();
}