namespace Jew.Domain.ProductInventory.Exceptions;

[System.Serializable]
public class CategoryException : InventoryException
{
    public CategoryException() { }
    public CategoryException(string message) : base(message) { }
    public CategoryException(string message, System.Exception inner) : base(message, inner) { }
    protected CategoryException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public CategoryException(string message, string field) : base(message, field){}

}