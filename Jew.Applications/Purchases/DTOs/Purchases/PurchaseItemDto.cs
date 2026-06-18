using Jew.Domain.Purchases.Transactions;

namespace Jew.Applications.Purchases.DTOs.Purchases;

public sealed record PurchaseItemDto(string ProductId, int Quantity, decimal UnitCost)
{
    public static PurchaseItem To(PurchaseItemDto dto)
    => new(dto.ProductId, dto.Quantity, dto.UnitCost);

    public static PurchaseItemDto From(PurchaseItem entity)
    => new(entity.ProductId, entity.Quantity, entity.UnitCost);
}