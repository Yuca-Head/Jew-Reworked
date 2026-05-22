using Jew.Domain.Shared.Exceptions;

namespace Jew.Domain.Purchases.Exceptions;

[System.Serializable]
public class PurchaseException : DomainException
{
    public PurchaseException() { }
    public PurchaseException(string message) : base(message) { }
    public PurchaseException(string message, System.Exception inner) : base(message, inner) { }
    protected PurchaseException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public PurchaseException(string message, string field) : base(message, field){}
}