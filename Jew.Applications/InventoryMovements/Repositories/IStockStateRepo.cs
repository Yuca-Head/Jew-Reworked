using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Domain.InventoryMovements.Repositories;

public interface IStockStateRepo : IRepository<ProductStockState, string>
{
    Task<ProductStockState> GetOrCreateAsync(string productId);

    Task ApplyMovementAsync(InventoryMovement movement);

    Task ApplyMovementsAsync(IEnumerable<InventoryMovement> movements);

    #pragma warning disable
    [Obsolete("Este método no debería ser utilizado, Mejor usar GetOrCreate o ApplyMovement")]
    static Task AddAsync(ProductStockState stockState)
    =>  throw new NotSupportedException(
        "StockState cannot be added manually. It is derived from movements.");
    #pragma warning enable
}