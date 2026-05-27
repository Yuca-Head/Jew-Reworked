using Jew.Domain.Sales.Transactions;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models.Sales;

namespace Jew.Infrastructure.Persistence.Mappers.Sales;

public sealed class SaleMapper : IMapper<Sale, SaleData>
{
    private static readonly SaleItemMapper _itemMapper = new();

    public Sale ToEntity(SaleData data)
    => new(data.ClientKey, [..data.Items.Select(_itemMapper.ToEntity)], data.TransactionId, data.Date);

    public SaleData ToModel(Sale domain)
    => new(domain.ClientKey, [..domain.Items.Select(_itemMapper.ToModel)], domain.TransactionId, domain.Date);
}