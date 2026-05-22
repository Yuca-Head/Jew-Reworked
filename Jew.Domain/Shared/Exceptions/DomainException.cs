using System.Numerics;

namespace Jew.Domain.Shared.Exceptions;

[System.Serializable]
public class DomainException : System.Exception
{
    public DomainException() { }
    public DomainException(string message) : base(message) { }
    public DomainException(string message, System.Exception inner) : base(message, inner) { }
    protected DomainException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public DomainException(string message, string fieldName) : base (message) {FieldName = fieldName;}

    private string _fieldName;
    public string FieldName {get => _fieldName.ToLower(); protected set => _fieldName = value;}

    public static DomainException ObjectNotFound(string paramName)
    => new($"{paramName} No encontrado", paramName);


}