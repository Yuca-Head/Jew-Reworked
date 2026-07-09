using Jew.Applications.Sales.Repositories;
using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Persistence.Mappers.Sales;
using Jew.Infrastructure.Persistence.Models.Sales;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonClientsRepo(Enums.Environment environment, string? fileName = null) :
JsonRepository<CodeKey, Client, ClientData>(fileName ?? defaultFileName, new ClientMapper(), environment), IClientRepo
{
    private const string defaultFileName = "Clients.json";
    protected override InMemoryClients InMemoryRepo {get;} = new(new(new CodeKeyComparer()));

    public Task<IEnumerable<Client>> GetByNameAsync(string name)
    => InMemoryRepo.GetByNameAsync(name);
}