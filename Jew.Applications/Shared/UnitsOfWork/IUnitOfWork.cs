using Jew.Applications.InventoryMovements.Repositories;
using Jew.Applications.ProductInventory.Repositories;
using Jew.Applications.Purchases.Repositories;
using Jew.Applications.Sales.Repositories;
using Jew.Domain.InventoryMovements.Repositories;


namespace Jew.Applications.Shared.UnitsOfWork;

public interface IUnitOfWork
{
    ICategoriesRepo Categories { get; }
    IProductsRepo Products { get; }
    ISuppliersRepo Suppliers { get; }
    IMovementsRepo Movements { get; }
    IStockStateRepo StockState { get; }
    ISalesRepo Sales { get; }   
    IPurchasesRepo Purchases { get; }
    ISupplierProductsRepo SuppliersProducts {get;}
    IClientRepo Clients {get;}

    //No sé que hace pero cuando lo sepa lo usaré.
    virtual void Commit()
    => throw new NotImplementedException();
    Task SaveChangesAsync();
    Task LoadAsync();
}