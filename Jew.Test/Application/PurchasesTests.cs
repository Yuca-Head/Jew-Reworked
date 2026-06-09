
using Jew.Applications.ProductInventory.Queries;
using Jew.Applications.Purchases.Queries;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Test.Application;

public class PurchasesTests
{
    [Fact]
    public void CanGetProductsFromSupplier()
    {
        JsonUnitOfWork context = new(Jew.Infrastructure.Enums.Environment.Test);
        context.Load();
        ProductQueryService productQuery = new(context);
        SupplierQueryService query = new(context, productQuery);
        
        Assert.True(query.GetProductsFromSupplier(1).Any());
    }
}