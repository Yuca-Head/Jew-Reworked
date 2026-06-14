using Jew.Domain.Sales.Entities;
using Jew.Domain.Sales.Repositories;
using Jew.Domain.Shared.Keys;
using Jew.Domain.Shared.People;

namespace Jew.Infrastructure.Repositories.InMemory;


public sealed class InMemoryClients(Dictionary<CodeKey, Client> entities) : InMemoryRepository<Client, CodeKey>(entities), IClientRepo
{
    public override void Add(Client entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if(entity.Key.Length != 5)
            throw new PersonException("La longitud del código del cliente debe ser mayor que 5");

        if(!_entities.TryAdd(entity.Key, entity))
            throw new PersonException("Ya existe un cliente con ese identificador");
    }

    public IEnumerable<Client>? GetByName(string name)
    => _entities.Values.Where(c => string.Equals(name, c.Name, StringComparison.OrdinalIgnoreCase));
}