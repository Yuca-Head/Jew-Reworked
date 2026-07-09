
using Jew.Applications.Purchases.Repositories;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Shared.Exceptions;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Repositories.Shared;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemorySuppliers(Dictionary<CodeKey, Supplier> entities) :
InMemoryRepository<Supplier, CodeKey>(entities), ISuppliersRepo
{

    protected override SupplierException ValidatorException => new($"Ya existe un proveedor con ese código: {code}");
    private static string code;
    public override async Task AddAsync(Supplier entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        code = entity.Key.ToString();
        await Validator(entity);
        _entities.Add(entity.Key, entity);
    }

    public override async Task AddAsync(IEnumerable<Supplier> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        await Validator(entities);

        foreach(var entity in entities)
            _entities.Add(entity.Key, entity);
    }


    public Task<IEnumerable<Supplier>> GetByNameAsync(string name)
    => Task.FromResult(_entities.Values.Where(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)));

    internal IEnumerable<Supplier>? GetByName(string name)
    {
        throw new NotImplementedException();
    }
}