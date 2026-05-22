

using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;

namespace Jew.Infrastructure.Repositories;

public class InMemoryMovements : IMovementsRepo
{
    private readonly Dictionary<int, InventoryMovement> _movements = []; 
    private int idCount = 1;
    public void Add(InventoryMovement entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var id = idCount++;

        _movements.Add(id, new(entity.ProductId, entity.Quantity, entity.UnitCost, entity.MovementType, entity.Party, entity.TransactionId){Key = id});
    }

    public bool Exist(int key)
    => _movements.ContainsKey(key);

    public IEnumerable<InventoryMovement> GetAll()
    => [.. _movements.Values];

    public InventoryMovement GetById(int id)
    => _movements.GetValueOrDefault(id);

    public IEnumerable<InventoryMovement> GetByTransactionId(Guid id)
    => GetAll().Where(m => m.TransactionId == id).ToList();

    public IEnumerable<InventoryMovement> GetByType(MovementType type)
    => _movements.Values.Where(m => m.MovementType == type);

    public IEnumerable<Guid> GetTransactionsId()
    => _movements.Values.Select(m => m.TransactionId).Distinct();

}