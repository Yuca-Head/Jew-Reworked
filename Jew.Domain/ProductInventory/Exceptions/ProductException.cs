using System.Globalization;
using System.Numerics;

namespace Jew.Domain.ProductInventory.Exceptions;

[System.Serializable]
public class ProductException : InventoryException
{
    public ProductException() { }
    public ProductException(string message) : base(message) { }
    public ProductException(string message, Field field) : base(message){FieldName = field.ToString(); AffectedField = field;} 
    public ProductException(string message, string field) : base(message){FieldName = field; AffectedField = Enum.Parse<Field>(field);}
    public ProductException(string message, System.Exception inner) : base(message, inner) { }

    public Field AffectedField {get; protected set;}
    public enum Field
    {
        code,
        name,
        price,
        state
    }   

    public static string GetFieldName(Field field)
    => field.ToString().ToLower();
}