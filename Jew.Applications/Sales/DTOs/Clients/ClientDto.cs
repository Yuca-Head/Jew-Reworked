using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Sales.DTOs.Clients;

public sealed record ClientDto (CodeKey Code, string Name)
{
    public static ClientDto From(Client client)
    => new(client.Key, client.Name);

}
