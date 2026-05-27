using Jew.Domain.Purchases.Entities;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.Purchases;

namespace Jew.Infrastructure.Persistence.Mappers.Purchases;

public class SupplierMapper : IMapper<Supplier, SupplierData>
{
    public Supplier ToEntity(SupplierData data)
    => new(data.Id, data.Name);

    public SupplierData ToModel(Supplier domain)
    => new(domain.Key, domain.Name);
}