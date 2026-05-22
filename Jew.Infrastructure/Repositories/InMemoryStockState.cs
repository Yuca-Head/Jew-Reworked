

using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;

namespace Jew.Infrastructure.Repositories;

public class InMemoryStockState : IStockStateRepo
{

    private readonly Dictionary<string, ProductStockState> _states = [];

    public ProductStockState Get(string productId)
    {
        if (!_states.TryGetValue(productId, out var state))
            throw new KeyNotFoundException($"No stock state for product {productId}");

        return state;
    }

    public ProductStockState GetOrCreate(string key)
    {
        if (!_states.TryGetValue(key, out var state))
        {
            state = new ProductStockState(key, 0, 0);   
            _states.Add(key, state);
        }

        return state;
    }
    
    public void Add(ProductStockState item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (_states.ContainsKey(item.Key))
            throw new InvalidOperationException($"Stock state for product {item.Key} already exists.");
        
        _states.Add(item.Key, item);
    }

    public IEnumerable<ProductStockState> GetAll()
    => _states.Values.ToList();


    public ProductStockState GetById(string id)
    => _states.GetValueOrDefault(id) ?? throw new Exception($"No stock state found with id {id}");

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

    public bool Exist(string key)
    => _states.ContainsKey(key);
}