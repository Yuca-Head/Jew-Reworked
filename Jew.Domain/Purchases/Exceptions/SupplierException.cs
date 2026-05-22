using Jew.Domain.Shared.Exceptions;

namespace Jew.Domain.Purchases.Exceptions;

[System.Serializable]
public class SupplierException : DomainException
{
    public SupplierException() { }
    public SupplierException(string message) : base(message) { }
    public SupplierException(string message, System.Exception inner) : base(message, inner) { }
    protected SupplierException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    public SupplierException(string message, string field) : base(message, field){}
}