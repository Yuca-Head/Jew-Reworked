using Jew.Applications.Purchases.Repositories;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Exceptions;
using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemorySupplierProducts(Dictionary<SupplierProductPK, SupplierProduct> entities) 
: InMemoryRepository<SupplierProduct, SupplierProductPK>(entities), ISupplierProductsRepo
{
    protected override SupplierException ValidatorException => new($"Este proveedor ya contiene este producto: {code}");
    private static string code;
    public override async Task AddAsync(SupplierProduct entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await Validator(entity);
        code = entity.Key.ToString();
        _entities.Add(entity.Key, entity);
    }

    public override async Task AddAsync(IEnumerable<SupplierProduct> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        await Validator(entities);

        foreach(var entity in entities)
            _entities.Add(entity.Key, entity);
    }

    public Task<IEnumerable<SupplierProduct>> GetByProductIdAsync(string productId)
    => Task.FromResult(_entities.Values.Where(x => x.ProductId == productId));

    public Task<IEnumerable<SupplierProduct>> GetBySupplierIdAsync(CodeKey supplierId)
    => Task.FromResult(_entities.Values.Where(x => x.SupplierKey == supplierId));


}