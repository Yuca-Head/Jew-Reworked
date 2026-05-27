using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Shared.People;

namespace Jew.Infrastructure.Persistence.Models.InventoryMovements;

public sealed record MovementData
(int Id, string ProductId, int Quantity, decimal UnitCost, MovementType MovementType, IParty? Party, Guid TransactionId);