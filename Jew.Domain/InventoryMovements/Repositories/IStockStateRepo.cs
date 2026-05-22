using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Domain.InventoryMovements.Repositories;

public interface IStockStateRepo : IRepository<ProductStockState, string>
{
    ProductStockState GetOrCreate(string productId);

    void ApplyMovement(InventoryMovement movement);

    [Obsolete("Este método no debería ser utilizado, Mejor usar GetOrCreate o ApplyMovement")]
    static void Add(ProductStockState stockState)
    =>  throw new NotSupportedException(
        "StockState cannot be added manually. It is derived from movements.");
}