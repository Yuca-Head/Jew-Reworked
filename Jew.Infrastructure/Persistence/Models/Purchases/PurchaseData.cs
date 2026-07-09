using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Persistence.Models.Purchases;

public sealed record PurchaseData
(CodeKey SupplierId, IReadOnlyList<PurchaseItemData> Items , Guid TransactionId, string? Description, DateTime Date);