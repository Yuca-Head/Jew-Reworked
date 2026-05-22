using Jew.Domain.Shared.Exceptions;

namespace Jew.Domain.Sales.Exceptions;

[System.Serializable]
public class SaleException : DomainException
{
    public SaleException() { }
    public SaleException(string message) : base(message) { }
    public SaleException(string message, System.Exception inner) : base(message, inner) { }
    protected SaleException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public SaleException(string message, string field) : base(message, field){}
}