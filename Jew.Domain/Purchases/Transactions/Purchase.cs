

using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Purchases.Transactions;

public readonly record struct Purchase 
(CodeKey SupplierId, IEnumerable<PurchaseItem> Items , Guid TransactionId, string? Description, DateTime Date = default) :
IIsTransaction, IHasPK<Guid>
{
    public Guid Key => TransactionId;
    
}
