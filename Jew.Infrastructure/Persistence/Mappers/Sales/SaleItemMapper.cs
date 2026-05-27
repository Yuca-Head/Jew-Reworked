using Jew.Domain.Sales.Transactions;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models.Sales;

namespace Jew.Infrastructure.Persistence.Mappers.Sales;

public sealed class SaleItemMapper : IMapper<SaleItem, SaleItemData>
{
    public SaleItem ToEntity(SaleItemData data)
    => new(data.ProductId, data.Quantity, data.UnitPrice);

    public SaleItemData ToModel(SaleItem domain)
    => new(domain.ProductId, domain.Quantity, domain.UnitPrice);
}