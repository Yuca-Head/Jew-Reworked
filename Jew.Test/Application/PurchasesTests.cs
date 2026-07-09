
using System.Threading.Tasks;
using Jew.Applications.ProductInventory.Queries;
using Jew.Applications.Purchases.Queries;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models.Purchases;
using Jew.Infrastructure.Persistence.Serialization;
using Jew.Infrastructure.UnitsOfWork;

namespace Jew.Test.Application;

public class PurchasesTests
{
    [Fact]
    public async Task CanGetProductsFromSupplier()
    {
        JsonUnitOfWork context = new(Jew.Infrastructure.Enums.Environment.Test);
        await context.Suppliers.LoadAsync();
        Assert.Equal(new(5,"PENES"), (await context.Suppliers.GetAllAsync()).First().Key);

        ProductQueryService productQuery = new(context);
        SupplierQueryService query = new(context, productQuery);
        
       
    }

    [Fact]
    public async Task CodeKeySerialize()
    {
        JsonStorageService<Cagadon> json = new(AppPaths.GetDataFilePath(Infrastructure.Enums.Environment.Test, "Cagadon.json"));
        JsonStorageService<SupplierProductData> json1 = 
        new(AppPaths.GetDataFilePath(Infrastructure.Enums.Environment.Test, "SupplierProducts.json"));
        var caca = await json.LoadAsync();

        SuppProductMapper mapper = new();

        var x = caca.Select(mapper.ToModel);

        await json1.SaveAsync(x);
    }

    [Fact]
    public async Task BuyWorking()
    {
        
    }

}


public class SuppProductMapper : IMapper<Cagadon, SupplierProductData>
{
    public Cagadon ToEntity(SupplierProductData data)
    => new(data.ProductPK.ProductId, data.ProductPK.SupplierId, data.Price);

    public SupplierProductData ToModel(Cagadon domain)
    => new(new(domain.SupplierId, domain.ProductId), domain.Price);
}

public readonly record struct Cagadon(string ProductId, CodeKey SupplierId, decimal Price);