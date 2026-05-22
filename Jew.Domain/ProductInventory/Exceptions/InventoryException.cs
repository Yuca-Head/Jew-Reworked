using Jew.Domain.Shared.Exceptions;

namespace Jew.Domain.ProductInventory.Exceptions;

[System.Serializable]
public class InventoryException : DomainException
{
    public InventoryException() { }
    public InventoryException(string message) : base(message) { }
    public InventoryException(string message, System.Exception inner) : base(message, inner) { }
    protected InventoryException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    

    public InventoryException(string message, string field) : base(message, field){}

}