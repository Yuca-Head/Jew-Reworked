namespace Jew.Infrastructure.Persistence.Models.Purchases;

public sealed record PurchaseItemData(string ProductId, int Quantity, decimal UnitCost);