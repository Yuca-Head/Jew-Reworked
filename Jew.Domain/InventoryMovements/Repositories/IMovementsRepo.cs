
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Domain.InventoryMovements.Repositories;
public interface IMovementsRepo : IRepository<InventoryMovement, int>
{
    IEnumerable<InventoryMovement> GetByTransactionId(Guid Id);
    IEnumerable<InventoryMovement> GetByType(MovementType type);
    IEnumerable<Guid> GetTransactionIds();
}
