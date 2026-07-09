

using Jew.Domain.Shared.Contacts;
using Jew.Domain.Shared.Keys;
using Jew.Domain.Shared.People;

namespace Jew.Domain.Purchases.Entities;

public class Supplier: Person<CodeKey>, IParty, IHasPK<CodeKey>
{
    public Supplier(CodeKey id, string name) : base (id, name){}
    public Supplier(Supplier supplier) : base(supplier){}
}

