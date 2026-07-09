
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Applications.InventoryMovements.Repositories;
public interface IMovementsRepo : IRepository<InventoryMovement, int>
{
    Task<IEnumerable<InventoryMovement>> GetByTransactionIdAsync(Guid Id);
    Task<IEnumerable<InventoryMovement>> GetByTypeAsync(MovementType type);
    Task<IEnumerable<Guid>> GetTransactionIdsAsync();
}
