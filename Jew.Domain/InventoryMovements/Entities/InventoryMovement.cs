
using Jew.Domain.InventoryMovements.Exceptions;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;
using Jew.Domain.Shared.People;

namespace Jew.Domain.InventoryMovements.Entities;

public enum MovementType
{
    In,
    Out ,
}

public readonly record struct InventoryMovement : IHasId, IIsTransaction
{
    public string ProductId { get; init;}
    public int Quantity { get; init;}
    public decimal UnitCost { get; init;}
    public DateTime Date { get; init;}
    public MovementType MovementType { get; init;}
    public Guid TransactionId {get; init;}
    public readonly int Key{get; init;}
    
    public string? Description {get; init;}

    public int SignedQuantity =>
        MovementType == MovementType.In ? Quantity : -Quantity;

    public decimal Total =>
        Quantity * UnitCost;


    public InventoryMovement(string productId, int quantity, decimal unitCost, MovementType movementType, Guid transactionId)
    {
        if(quantity <= 0)
            throw new InventoryMovementException("Debe ingresar al menos un producto para realizar el movimiento", nameof(Quantity));
        if(unitCost <= 0)
            throw new InventoryMovementException("El costo no del movimiento no puede ser de 0", nameof(UnitCost));
        

        ProductId = productId;
        UnitCost = unitCost;
        Quantity = quantity;
        MovementType = movementType;
        Date = DateTime.Now;
        TransactionId = transactionId;
    }

}