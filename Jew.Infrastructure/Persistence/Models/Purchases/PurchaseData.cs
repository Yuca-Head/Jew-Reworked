namespace Jew.Infrastructure.Persistence.Models.Purchases;

public sealed record PurchaseData
(int SupplierId, IReadOnlyList<PurchaseItemData> Items , Guid TransactionId, DateTime Date);