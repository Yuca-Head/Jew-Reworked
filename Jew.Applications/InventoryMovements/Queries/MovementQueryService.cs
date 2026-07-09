using System.Threading.Tasks;
using Jew.Applications.InventoryMovements.DTOs;
using Jew.Applications.InventoryMovements.Mappers;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.ProductInventory.Entities;

namespace Jew.Applications.InventoryMovements.Queries;

public sealed class MovementQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public Task<IEnumerable<MovementDto>> GetMovements()
        => _context.Movements.GetAllAsync()
            .ContinueWith(t => MovementMappers.ConvertMovementsToDto(t.Result));

    public Task<IEnumerable<MovementDto>> GetMovementsByTransactionId(Guid id)
        => _context.Movements.GetByTransactionIdAsync(id)
            .ContinueWith(t => MovementMappers.ConvertMovementsToDto(t.Result));

    public Task<IEnumerable<Guid>> GetTransactionIds()
        => _context.Movements.GetTransactionIdsAsync();

    public async Task<MovementDto> GetMovementByID(int id)
    {
        var entity = await _context.Movements.GetByIdAsync(id);
        return MovementDto.From(entity);
    }

}