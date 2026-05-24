namespace Jew.Infrastructure.UnitOfWork;

public class JSonUnitOfWork : UnitOfWork
{
    public JSonUnitOfWork
    (JSonCategoriesRepo categories, JSonProductsRepo products, JSonSuppliersRepo suppliers, JSonMovementsRepo movements, JSonStockStateRepo stockState, JSonSaleRepo sales, JSonPurchaseRepo purchases)
        : base(categories, products, suppliers, movements, stockState, sales, purchases)
    {
    }
}