using Jew.Domain.Shared.People;

namespace Jew.Domain.Sales.Exceptions;

[System.Serializable]
public class ClientException : PersonException
{
    public ClientException() { }
    public ClientException(string message) : base(message) { }
    public ClientException(string message, System.Exception inner) : base(message, inner) { }
    protected ClientException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}