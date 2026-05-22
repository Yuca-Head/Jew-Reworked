using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Shared.Contacts;

public interface IContact : IHasId
{
    string Get();
    void Modify(string nuevo);
    string Type{get;}

    new int Id {get; set;}

}
