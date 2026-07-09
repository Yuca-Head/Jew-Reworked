

using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Shared.Exceptions;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryStockState(Dictionary<string, ProductStockState> entities) :
 InMemoryRepository<ProductStockState, string>(entities), IStockStateRepo
{

    public ProductStockState Get(string productId)
    {
        if (!_entities.TryGetValue(productId, out var state))
            throw new ProductException($"Sin historial de producto encontrado. Id del producto: {productId}");

        return state;
    }

    public Task<ProductStockState> GetOrCreateAsync(string key)
    {
        if (!_entities.TryGetValue(key, out var state))
        {
            state = new ProductStockState(key, 0, 0);   
            _entities.Add(key, state);
        }

        return Task.FromResult(state);
    }
    
    public override Task AddAsync(ProductStockState item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (_entities.ContainsKey(item.Key))
            throw new InvalidOperationException($"Stock state for product {item.Key} already exists.");
        
        _entities.Add(item.Key, item);

        return Task.CompletedTask;
    }

    public async Task ApplyMovementAsync(InventoryMovement movement)
    {
        var state = await GetOrCreateAsync(movement.ProductId);
        switch (movement.MovementType)
        {
            case MovementType.In:
                state.AddQuantity(movement.Quantity, movement.UnitCost);
            break;

            case MovementType.Out:
                state.ReduceQuantity(movement.Quantity);
            break;
        }
    
    }

    public async Task ApplyMovementsAsync(IEnumerable<InventoryMovement> movements)
    {
        var tasks = movements.Select(ApplyMovementAsync);

        await Task.WhenAll(tasks);
    }

    public override async Task AddAsync(IEnumerable<ProductStockState> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        foreach(var entity in entities)
        {
            if (_entities.ContainsKey(entity.Key))
                throw new InvalidOperationException($"Stock state for product {entity.Key} already exists.");
            _entities.Add(entity.Key, entity);
        }
    }


}