namespace Jew.Infrastructure.Persistence.Models.InventoryMovements;

public sealed record ProductStockStateData(string ProductId, int Quantity, decimal AverageCost);