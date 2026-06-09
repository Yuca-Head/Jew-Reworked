using Jew.Domain.Purchases.Transactions;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.Purchases;

namespace Jew.Infrastructure.Persistence.Mappers.Purchases;

public sealed class PurchaseMapper : IMapper<Purchase, PurchaseData>
{
    private readonly static PurchaseItemMapper _itemMapper = new();
    public Purchase ToEntity(PurchaseData data)
    => new(data.SupplierId, [.. data.Items.Select(_itemMapper.ToEntity)], data.TransactionId, data.Description, data.Date);

    public PurchaseData ToModel(Purchase domain)
    => new(domain.SupplierId, [.. domain.Items.Select(_itemMapper.ToModel)], domain.TransactionId, domain.Description, domain.Date);
}