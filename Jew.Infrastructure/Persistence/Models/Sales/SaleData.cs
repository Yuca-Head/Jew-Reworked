using Jew.Domain.Sales.Entities;
using Jew.Domain.Sales.Transactions;
using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Persistence.Models.Sales;

public sealed record SaleData (CodeKey ClientKey, IReadOnlyList<SaleItemData> Items, Guid TransactionId, DateTime Date);