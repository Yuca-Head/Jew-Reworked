
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Repositories;
using Jew.Infrastructure.Repositories.Test;
using Jew.Infrastructure.Shared;


namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemorySuppliers(Dictionary<int, Supplier> entities) : InMemoryRepository<Supplier, int>(entities), ISuppliersRepo
{

    private readonly IncrementalKeyGenerator _identity = new(IncrementalKeyGenerator.GetLastKey(entities.Keys));
    public override void Add(Supplier entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        entity.SetId(_identity.Next(this));
        _entities.Add(entity.Key, entity);
    }

    public Supplier? GetByName(string name)
    => _entities.Values.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
}