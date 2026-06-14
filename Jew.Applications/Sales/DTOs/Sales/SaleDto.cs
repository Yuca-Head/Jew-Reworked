using Jew.Domain.Sales.Transactions;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Sales.DTOs.Sales;

public sealed record SaleDto
    (CodeKey ClientKey, IEnumerable<SaleItemDto> Items, Guid TransactionId, string? Description, DateTime Date)
{
    public static SaleDto From(Sale sale)
    => new(sale.ClientKey, sale.Items.Select(SaleItemDto.From), sale.TransactionId, sale.Description, sale.Date);
    public static Sale To(SaleDto dto)
    => new(dto.ClientKey, [..dto.Items.Select(SaleItemDto.To)], dto.TransactionId, dto.Description, dto.Date);
}
