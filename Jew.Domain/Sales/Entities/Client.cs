using Jew.Domain.Shared.Contacts;
using Jew.Domain.Shared.Keys;
using Jew.Domain.Shared.People;


namespace Jew.Domain.Sales.Entities;

public class Client(CodeKey code, string name) : Person<CodeKey>(code, name), IParty;

