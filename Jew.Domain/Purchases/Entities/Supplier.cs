

using Jew.Domain.Shared.Contacts;
using Jew.Domain.Shared.People;

namespace Jew.Domain.Purchases.Entities;

public class Supplier: Person<int>, IParty
{
    public Supplier(int id, string name) : base (id, name){}
    public Supplier(Supplier supplier) : base(supplier){}

    internal void SetId(int id)
    {
        if(Key != 0)
            throw new InvalidOperationException("ID ya ha sido asignado.");
        Key = id;
    }
    
}

