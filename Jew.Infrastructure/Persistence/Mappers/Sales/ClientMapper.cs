using Jew.Domain.Sales.Entities;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models.Sales;

namespace Jew.Infrastructure.Persistence.Mappers.Sales;

public sealed class ClientMapper : IMapper<Client, ClientData>
{
    public Client ToEntity(ClientData data)
    => new(data.Key, data.Name);
    public ClientData ToModel(Client domain)
    => new(domain.Key, domain.Name);
}