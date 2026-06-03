using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.InventoryMovements.Queries;

public sealed class StockStateQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public decimal GetProductCost(string code)
    => _context.StockState.GetById(code)?.AverageCost ?? 0;

    public int GetStock(string code)
    => _context.StockState.GetById(code)?.Quantity ?? 0;
}