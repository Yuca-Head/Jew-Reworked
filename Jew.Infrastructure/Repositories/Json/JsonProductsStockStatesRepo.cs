using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Infrastructure.Persistence.Mappers;
using Jew.Infrastructure.Persistence.Mappers.InventoryMovements;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.InventoryMovements;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonProductsStockStates
(Enums.Environment environment, string? fileName = null) 
: JsonRepository<string, ProductStockState, ProductStockStateData>
(fileName ?? defaultFileName, new ProductStockStateMapper(), environment),IStockStateRepo
{
    protected override InMemoryStockState InMemoryRepo {get;} = new([]);

    private const string defaultFileName = "ProductStockStates.json";

    public Task ApplyMovementAsync(InventoryMovement movement)
    => InMemoryRepo.ApplyMovementAsync(movement);

    public Task<ProductStockState> GetOrCreateAsync(string productId)
    => InMemoryRepo.GetOrCreateAsync(productId);

    public Task ApplyMovementsAsync(IEnumerable<InventoryMovement> movements)
    => InMemoryRepo.ApplyMovementsAsync(movements);
}