

using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Purchases.Transactions;

public readonly record struct Purchase 
(int SupplierId, IReadOnlyList<PurchaseItem> Items , Guid TransactionId, DateTime Date = default) :
IIsTransaction, IHasPK<Guid>
{
    public Guid Key => TransactionId;
}
