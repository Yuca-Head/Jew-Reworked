using Jew.Applications.InventoryMovements.DTOs;
using Jew.Applications.InventoryMovements.Mappers;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.ProductInventory.Entities;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.InventoryMovements.Queries;

public sealed class MovementQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;



    public IEnumerable<MovementDto> GetMovements()
    => MovementMappers.ConvertMovementsToDto(_context.Movements.GetAll());
    public IEnumerable<MovementDto> GetMovementsByTransactionId(Guid id)
    =>  MovementMappers.ConvertMovementsToDto(_context.Movements.GetByTransactionId(id));
    public IEnumerable<MovementDto> GetMovementsByDate(DateTime date)
    => MovementMappers.ConvertMovementsToDto(_context.Movements.GetAll().Where(m => m.Date == date));
    public IEnumerable<Guid> GetTransactionsId()
    => _context.Movements.GetTransactionsId();
    public MovementDto GetMovementByID(int iD)
    => MovementDto.From(_context.Movements.GetById(iD));

}