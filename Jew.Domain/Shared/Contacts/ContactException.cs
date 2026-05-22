

using Jew.Domain.Shared.Exceptions;

namespace Jew.Domain.Shared.Contacts;

[System.Serializable]
public class ContactException : DomainException
{
    public ContactException() { }
    public ContactException(string message) : base(message) { }
    public ContactException(string message, System.Exception inner) : base(message, inner) { }

}