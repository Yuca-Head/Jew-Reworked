

using Jew.Domain.Shared.Exceptions;

namespace Jew.Domain.InventoryMovements.Exceptions;

[System.Serializable]
public class InventoryMovementException : DomainException
{
    public InventoryMovementException() { }
    public InventoryMovementException(string message) : base(message) { }
    public InventoryMovementException(string message, System.Exception inner) : base(message, inner) { }
    public InventoryMovementException(string message, string field) : base(message, field){}
}