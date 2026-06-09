using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.People;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models.InventoryMovements;

namespace Jew.Infrastructure.Persistence.Mappers.InventoryMovements;

public sealed class MovementMapper : IMapper<InventoryMovement, MovementData>
{
    public InventoryMovement ToEntity(MovementData data)
    => new(data.ProductId, data.Quantity, data.UnitCost, data.MovementType, data.TransactionId){ Key = data.Id}; 
    public MovementData ToModel(InventoryMovement domain)
    => new(domain.Key, domain.ProductId, domain.Quantity, domain.UnitCost, domain.MovementType,
    domain.TransactionId);    
}