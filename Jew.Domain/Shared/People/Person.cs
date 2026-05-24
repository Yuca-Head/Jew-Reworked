using System.ComponentModel.DataAnnotations;
using Jew.Domain.Shared.Contacts;
using Jew.Domain.Shared.Exceptions;
using Jew.Domain.Shared.Keys;
namespace Jew.Domain.Shared.People;

/// <summary>
/// Person.
/// </summary>
/// <typeparam name="T">Primary Key type.</typeparam>
public abstract class Person<T> : IHasPK<T>
{
    public virtual T Key {get; protected set;}

    public readonly PersonContacts Contacts = new();

    private string _name = string.Empty;

    public string Name 
    { 
        get => _name; 
        protected set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Name));
            _name = value;
        }
    }

    public Person(T key, string name, ICollection<IContact>? contacts)
    {
        Key = key;
        Name = name;

        if(contacts is not null)
            foreach(var c in contacts)
                Contacts.AddContact(c);
    }

    public Person(Person<T> person)
    {
        Key = person.Key;
        Name = person.Name;
        Contacts = person.Contacts;
    }
}

[System.Serializable]
public class PersonException : DomainException
{
    public PersonException() { }
    public PersonException(string message) : base(message) { }
    public PersonException(string message, System.Exception inner) : base(message, inner) { }
    protected PersonException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}