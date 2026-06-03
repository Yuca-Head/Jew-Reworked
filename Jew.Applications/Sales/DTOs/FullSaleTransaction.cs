using Jew.Domain.Sales.Transactions;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Sales.DTOs;

public sealed record FullSaleTransactionDto
    (CodeKey ClientKey, IEnumerable<SaleItem> Items, Guid TransactionId, DateTime Date)
{
    public static FullSaleTransactionDto From(Sale sale)
    => new(sale.ClientKey, sale.Items, sale.TransactionId, sale.Date);
}