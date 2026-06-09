using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.ProductInventory.Entities;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.InventoryMovements.Commands;
public class MovementCommands(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;
    public void AddMovement(InventoryMovement movement)
    {
        _context.StockState.ApplyMovement(movement);
        _context.Movements.Add(movement);
    }

    public void RemoveMovements(params  IEnumerable<InventoryMovement> movements)
    {
        
    }
}
