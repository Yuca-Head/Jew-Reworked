namespace Jew.Infrastructure.Persistence.Models.Sales;

public sealed record SaleItemData(string ProductId, int Quantity, decimal UnitPrice);