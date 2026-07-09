using Jew.Applications.Sales.Repositories;
using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.Keys;
using Jew.Domain.Shared.People;

namespace Jew.Infrastructure.Repositories.InMemory;


public sealed class InMemoryClients(Dictionary<CodeKey, Client> entities) : InMemoryRepository<Client, CodeKey>(entities),
IClientRepo
{
    public override Task AddAsync(Client entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Validator(entity);

        return Task.CompletedTask;
    }

    public override Task AddAsync(IEnumerable<Client> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        Validator(entities);

        foreach(var entity in entities)
            _entities.Add(entity.Key, entity);

        return Task.CompletedTask;
    }

    protected override Task Validator(params IEnumerable<Client> entities)
    {
        foreach(var entity in entities)
        {
            if(entity.Key.Length != 5)
                throw new PersonException("La longitud del código del cliente debe ser mayor que 5");

            if(!_entities.TryAdd(entity.Key, entity))
                throw new PersonException("Ya existe un cliente con ese identificador");
        }

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Client>> GetByNameAsync(string name)
    => Task.FromResult(_entities.Values.Where(c => string.Equals(name, c.Name, StringComparison.OrdinalIgnoreCase)));

}