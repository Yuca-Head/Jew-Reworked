using Jew.Domain.Sales.Transactions;

namespace Jew.Applications.Sales.DTOs.Sales;

public sealed record SaleItemDto(string ProductId, decimal UnitPrice, int Quantity)
{
    public static SaleItemDto From(SaleItem entity)
    => new(entity.ProductId, entity.UnitPrice, entity.Quantity);
    public static SaleItem To(SaleItemDto dto)
    => new(dto.ProductId, dto.Quantity, dto.UnitPrice);
}