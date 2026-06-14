using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.InventoryMovements.Queries;

public sealed class StockStateQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public decimal GetProductCost(string code)
    => GetProductState(code)?.AverageCost ?? 0;

    public int GetStock(string code)
    => GetProductState(code)?.Quantity ?? 0;

    public IEnumerable<string> PurchasedOnes()
    => _context.StockState.GetAll().Select(x =>x.Key);

    public ProductStockState GetProductState(string code)
    => _context.StockState.GetById(code);
}