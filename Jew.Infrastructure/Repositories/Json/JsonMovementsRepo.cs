using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Infrastructure.Persistence.Mappers;
using Jew.Infrastructure.Persistence.Mappers.InventoryMovements;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.InventoryMovements;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonMovementsRepo(Enums.Environment environment, string? fileName = null) :
JsonRepository<int, InventoryMovement, MovementData>(fileName ?? defaultFileName, new MovementMapper(), environment), IMovementsRepo
{
    private const string defaultFileName = "Movements.json";

    protected override InMemoryMovements InMemoryRepo {get;} = new([]);

    public IEnumerable<InventoryMovement> GetByTransactionId(Guid id)
    => InMemoryRepo.GetByTransactionId(id);

    public IEnumerable<InventoryMovement> GetByType(MovementType type)
    => InMemoryRepo.GetByType(type);

    public IEnumerable<Guid> GetTransactionIds()
    => InMemoryRepo.GetTransactionIds();
}