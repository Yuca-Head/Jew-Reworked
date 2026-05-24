using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Domain.Purchases.Repositories;
using Jew.Domain.Sales.Repositories;

namespace Jew.Infrastructure.UnitOfWork;

public abstract class UnitOfWork : IUnitOfWork
{
    public UnitOfWork
    (ICategoriesRepo categories, IProductsRepo products, ISuppliersRepo suppliers, IMovementsRepo movements, IStockStateRepo stockState, ISaleRepo sales, IPurchaseRepo purchases)
    {
        ArgumentNullException.ThrowIfNull(categories);
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(suppliers);
        ArgumentNullException.ThrowIfNull(movements);
        ArgumentNullException.ThrowIfNull(stockState);
        ArgumentNullException.ThrowIfNull(sales);
        ArgumentNullException.ThrowIfNull(purchases);

        Categories = categories;
        Products = products;
        Suppliers = suppliers;
        Movements = movements;
        StockState = stockState;
        Sales = sales;
        Purchases = purchases;
    }

    public ICategoriesRepo Categories {get; }

    public IProductsRepo Products  {get; }

    public ISuppliersRepo Suppliers  {get; }

    public IMovementsRepo Movements  {get; }

    public IStockStateRepo StockState  {get; }

    public ISaleRepo Sales  {get; }

    public IPurchaseRepo Purchases  {get; }

    public void SaveChanges()
    {
        Categories.SaveChanges();
        Products.SaveChanges();
        Suppliers.SaveChanges();
        Movements.SaveChanges();
        StockState.SaveChanges();
        Sales.SaveChanges();
        Purchases.SaveChanges();
    }
}