using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Domain.Purchases.Repositories;
using Jew.Domain.Sales.Repositories;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.UnitOfWork;

public abstract class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="categories"></param>
    /// <param name="products"></param>
    /// <param name="suppliers"></param>
    /// <param name="movements"></param>
    /// <param name="stockState"></param>
    /// <param name="sales"></param>
    /// <param name="purchases"></param>
    public UnitOfWork
    (ICategoriesRepo categories, IProductsRepo products, ISuppliersRepo suppliers, 
    IMovementsRepo movements, IStockStateRepo stockState, ISalesRepo sales, IPurchasesRepo purchases,
    ISupplierProductsRepo suppliersProducts, IClientRepo clients)
    {
        ArgumentNullException.ThrowIfNull(categories);
        ArgumentNullException.ThrowIfNull(products);
        ArgumentNullException.ThrowIfNull(suppliers);
        ArgumentNullException.ThrowIfNull(movements);
        ArgumentNullException.ThrowIfNull(stockState);
        ArgumentNullException.ThrowIfNull(sales);
        ArgumentNullException.ThrowIfNull(purchases);
        ArgumentNullException.ThrowIfNull(suppliersProducts);
        ArgumentNullException.ThrowIfNull(clients);

        Categories = categories;
        Products = products;
        Suppliers = suppliers;
        Movements = movements;
        StockState = stockState;
        Sales = sales;
        Purchases = purchases;
        SuppliersProducts = suppliersProducts;
        Clients = clients;

        _repositories = 
        [Categories, Products, Suppliers, Movements, StockState, Sales, Purchases, SuppliersProducts, Clients];
    }

    public ICategoriesRepo Categories {get; }
    public IProductsRepo Products  {get; }
    public ISuppliersRepo Suppliers  {get; }
    public IMovementsRepo Movements  {get; }
    public IStockStateRepo StockState  {get; }
    public ISalesRepo Sales  {get; }
    public IPurchasesRepo Purchases  {get; }
    public ISupplierProductsRepo SuppliersProducts {get;}
    public IClientRepo Clients {get;}

    private readonly IRepository[] _repositories;

    public virtual void SaveChanges()
    => Array.ForEach(_repositories, x => x.SaveChanges());
    
    public virtual void Load()
    => Array.ForEach(_repositories, x => x.Load());
}