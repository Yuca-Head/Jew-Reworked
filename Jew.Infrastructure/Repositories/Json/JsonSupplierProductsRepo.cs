using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Repositories;
using Jew.Domain.Purchases.Transactions;
using Jew.Infrastructure.Persistence.Mappers.Purchases;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models.Purchases;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonSupplierProductsRepo
(Enums.Environment environment, string? fileName = null) : 
JsonRepository<SupplierProductPK, SupplierProduct, SupplierProductData>
(fileName ?? defaultFileName, new SupplierProductMapper(), environment), ISupplierProductsRepo
{
    private const string defaultFileName = "SupplierProducts.json";
    protected override InMemorySupplierProducts InMemoryRepo {get;} = new([]);

    public IEnumerable<SupplierProduct> GetByProductId(string productId)
    => InMemoryRepo.GetByProductId(productId);

    public IEnumerable<SupplierProduct> GetBySupplierId(int supplierId)
    => InMemoryRepo.GetBySupplierId(supplierId);
}