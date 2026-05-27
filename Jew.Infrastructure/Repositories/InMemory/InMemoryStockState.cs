

using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.ProductInventory.Exceptions;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryStockState(Dictionary<string, ProductStockState> entities) : InMemoryRepository<ProductStockState, string>(entities), IStockStateRepo
{
    public ProductStockState Get(string productId)
    {
        if (!_entities.TryGetValue(productId, out var state))
            throw new ProductException($"Sin historial de producto encontrado. Id del producto: {productId}");

        return state;
    }

    public ProductStockState GetOrCreate(string key)
    {
        if (!_entities.TryGetValue(key, out var state))
        {
            state = new ProductStockState(key, 0, 0);   
            _entities.Add(key, state);
        }

        return state;
    }
    
    public override void Add(ProductStockState item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (_entities.ContainsKey(item.Key))
            throw new InvalidOperationException($"Stock state for product {item.Key} already exists.");
        
        _entities.Add(item.Key, item);
    }

    public void ApplyMovement(InventoryMovement movement)
    {
        var state = GetOrCreate(movement.ProductId);
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

}