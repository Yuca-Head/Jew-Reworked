using Jew.Domain.Purchases.Entities;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.Purchases;

namespace Jew.Infrastructure.Persistence.Mappers.Purchases;

public sealed class SupplierProductMapper : IMapper<SupplierProduct, SupplierProductData>
{
    public SupplierProduct ToEntity(SupplierProductData data)
    => new(data.ProductPK, data.Price);
    public SupplierProductData ToModel(SupplierProduct domain)
    => new(domain.Key, domain.Price);
}   