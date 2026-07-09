using Jew.Applications.InventoryMovements.Repositories;
using Jew.Applications.ProductInventory.Repositories;
using Jew.Applications.Purchases.Repositories;
using Jew.Applications.Sales.Repositories;
using Jew.Applications.Shared.Common;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Shared.UnitsOfWork;

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

        _independentsRepositories = 
        [Categories, Suppliers, Clients];

    }

    public ICategoriesRepo Categories {get; }
    public IProductsRepo Products  {get; }
    public ISuppliersRepo Suppliers  {get; }
    public IMovementsRepo Movements  {get; }
    public IStockStateRepo StockState  {get; }
    public ISalesRepo Sales  {get; }
    public IPurchasesRepo Purchases  {get;} 
    public ISupplierProductsRepo SuppliersProducts {get;}
    public IClientRepo Clients {get;}

    private readonly IRepository[] _independentsRepositories;


    public virtual async Task SaveChangesAsync()
    {
        
        await Task.WhenAll(_independentsRepositories.Select(x => x.SaveChangesAsync()));
        await Products.SaveChangesAsync();
        await Task.WhenAll(SuppliersProducts.SaveChangesAsync(), Movements.SaveChangesAsync(), StockState.SaveChangesAsync());
        await Task.WhenAll(Purchases.SaveChangesAsync(), Sales.SaveChangesAsync());
        
    }
    
    public virtual async Task LoadAsync()
    {
        await Task.WhenAll(_independentsRepositories.Select(x => x.LoadAsync()));
        await Products.LoadAsync();
        await Task.WhenAll(SuppliersProducts.LoadAsync(), Movements.LoadAsync(), StockState.LoadAsync());
        await Task.WhenAll(Purchases.LoadAsync(), Sales.LoadAsync());
    }
}