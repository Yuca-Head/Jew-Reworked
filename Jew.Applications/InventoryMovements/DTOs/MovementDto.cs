using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Shared.People;

namespace Jew.Applications.InventoryMovements.DTOs;


public sealed record MovementDto(string ProductId, int Quantity, decimal UnitCost, MovementType MovementType,
IParty? Party, Guid TransactionId)
{
    public static MovementDto From(InventoryMovement movement)
    => 
    new (movement.ProductId, movement.Quantity, movement.UnitCost, movement.MovementType, movement.Party, movement.TransactionId);
}