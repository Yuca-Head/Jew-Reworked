

using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Infrastructure.Repositories.Shared;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryMovements(Dictionary<int, InventoryMovement> movements) : 
InMemoryRepository<InventoryMovement, int>(movements), IMovementsRepo
{
    private readonly IncrementalKeyGenerator _identity = new(IncrementalKeyGenerator.GetLastKey(movements.Keys));
    public override void Add(InventoryMovement entity)
    {
        _identity.Next(this);
        _entities.Add(_identity.CurrentKey, entity with { Key = _identity.CurrentKey });
    }

    public IEnumerable<InventoryMovement> GetByTransactionId(Guid id)
    => GetAll().Where(m => m.TransactionId == id);

    public IEnumerable<InventoryMovement> GetByType(MovementType type)
    => _entities.Values.Where(m => m.MovementType == type);

    public IEnumerable<Guid> GetTransactionIds()
    => _entities.Values.Select(m => m.TransactionId).Distinct();
}