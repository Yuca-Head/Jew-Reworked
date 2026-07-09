using System.Threading.Tasks;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.ProductInventory.Exceptions;

namespace Jew.Applications.InventoryMovements.Queries;

public sealed class StockStateQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public async Task<decimal> GetProductCost(string code)
    => (await GetProductState(code))?.AverageCost ?? 0;

    public async Task<int> GetStock(string code)
    => (await GetProductState(code))?.Quantity ?? 0;

    public async Task<IEnumerable<string>> PurchasedOnes()
    => (await _context.StockState.GetAllAsync()).Select(x =>x.Key);

    public async Task<ProductStockState> GetProductState(string code)
    =>(await _context.StockState.GetByIdAsync(code)) ?? new(code, 0, 0);
}