using Jew.Domain.Shared.People;
using Jew.Domain.Shared.Contacts;

namespace Jew.Domain.Shared.People;

public class PersonContacts
{
    private readonly Dictionary<int, IContact> _contacts = [];
    public IReadOnlyCollection<IContact> Contacts => [.._contacts.Values];

    private int _id = 1;

    public void AddContact(IContact contact)
    {   
    
        if(Contacts.Any(c => string.Equals(c.Get(), contact.Get(), StringComparison.OrdinalIgnoreCase)))
            throw new PersonException("Esta persona ya tiene este contacto");
        
        contact.Id = _id++;
        _contacts.Add(contact.Id, contact);
    }

    public void RemoveContact(int id)
    {
        if(!_contacts.Remove(id))
            throw new PersonException("Contacto no encontrado");
    }

    public IContact? GetContact(int id)
    => _contacts.GetValueOrDefault(id);

}