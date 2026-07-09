using System.Threading.Tasks;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.ProductInventory.Entities;


namespace Jew.Applications.InventoryMovements.Commands;
public class MovementCommands(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;
    public async Task AddMovement(InventoryMovement movement)
    {
        await _context.StockState.ApplyMovementAsync(movement);
        await _context.Movements.AddAsync(movement);
    }

    public async Task AddMovements(IEnumerable<InventoryMovement> movements)
    {
        await _context.StockState.ApplyMovementsAsync(movements);
        await _context.Movements.AddAsync(movements);
    }

    public void RemoveMovements(params  IEnumerable<InventoryMovement> movements)
    {
        
    }
}
