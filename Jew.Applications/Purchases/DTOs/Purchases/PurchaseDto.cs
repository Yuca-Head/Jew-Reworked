using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Purchases.DTOs.Purchases;

public sealed record PurchaseDto
(CodeKey SupplierId, IEnumerable<PurchaseItemDto> Items , Guid TransactionId, string? Description, DateTime Date)
{
    public static PurchaseDto From(Purchase entity)
    => new(entity.SupplierId, entity.Items.Select(PurchaseItemDto.From), entity.TransactionId, entity.Description, entity.Date);
    public static Purchase To(PurchaseDto dto)
    => new(dto.SupplierId, dto.Items.Select(PurchaseItemDto.To), dto.TransactionId, dto.Description, dto.Date);
}