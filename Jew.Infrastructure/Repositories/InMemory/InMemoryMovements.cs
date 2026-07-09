

using Jew.Applications.InventoryMovements.Repositories;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Infrastructure.Repositories.Shared;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryMovements(Dictionary<int, InventoryMovement> movements) : 
InMemoryRepository<InventoryMovement, int>(movements), IMovementsRepo
{
    private readonly IncrementalKeyGenerator _identity = new(IncrementalKeyGenerator.GetLastKey(movements.Keys));
    public override Task AddAsync(InventoryMovement entity)
    {
        Validator(entity);
        return Task.CompletedTask;
    }

    public override Task AddAsync(IEnumerable<InventoryMovement> entities)
    {
        Validator(entities);
        return Task.CompletedTask;
    }

    protected override Task Validator(params IEnumerable<InventoryMovement> entities)
    {
        foreach(var entity in entities)
        {
            _identity.Next(this);
            _entities.Add(_identity.CurrentKey, entity with { Key = _identity.CurrentKey });
        }

        return Task.CompletedTask;
    }

    public Task<IEnumerable<InventoryMovement>> GetByTransactionIdAsync(Guid id)
    => Task.FromResult(_entities.Values.Where(m => m.TransactionId == id));

    public Task<IEnumerable<InventoryMovement>> GetByTypeAsync(MovementType type)
    => Task.FromResult(_entities.Values.Where(m => m.MovementType == type));

    public Task<IEnumerable<Guid>> GetTransactionIdsAsync()
    => Task.FromResult(_entities.Values.Select(m => m.TransactionId).Distinct());
}