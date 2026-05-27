using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Purchases.Repositories;
using Jew.Domain.Purchases.Transactions;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemorySupplierProducts(Dictionary<SupplierProductPK, SupplierProduct> entities) : InMemoryRepository<SupplierProduct, SupplierProductPK>(entities), ISupplierProductsRepo
{
    public override void Add(SupplierProduct entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if(_entities.TryAdd(entity.Key, entity))
            throw new SupplierException("Este proveedor ya contiene este producto", nameof(entity.Key));
    }

    public IEnumerable<SupplierProduct> GetByProductId(string productId)
    => _entities.Values.Where(x => x.ProductId == productId);

    public IEnumerable<SupplierProduct> GetBySupplierId(int supplierId)
    => _entities.Values.Where(x => x.SupplierId == supplierId);
}