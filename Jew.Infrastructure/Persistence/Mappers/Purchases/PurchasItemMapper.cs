using Jew.Domain.Purchases.Transactions;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.Purchases;

namespace Jew.Infrastructure.Persistence.Mappers.Purchases;

public sealed class PurchaseItemMapper : IMapper<PurchaseItem, PurchaseItemData>
{
    public PurchaseItem ToEntity(PurchaseItemData data)
    => new(data.ProductId, data.Quantity, data.UnitCost);

    public PurchaseItemData ToModel(PurchaseItem domain)
    => new(domain.ProductId, domain.Quantity, domain.UnitCost);
}