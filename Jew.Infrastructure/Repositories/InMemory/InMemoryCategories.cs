
using Jew.Applications.ProductInventory.Repositories;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Shared.Exceptions;
using Jew.Infrastructure.Repositories.Shared;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryCategories() 
: InMemoryRepository<Category, string>(new Dictionary<string, Category>(StringComparer.OrdinalIgnoreCase)),
 ICategoriesRepo   
{

    protected override CategoryException ValidatorException => new("Ya existe una categoría con ese nombre");
    public override async Task AddAsync(Category entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await Validator(entity);

        _entities.Add(entity.Name, entity);
    }

    public override async Task AddAsync(IEnumerable<Category> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        await Validator(entities);

        foreach(var entity in entities)
            _entities.Add(entity.Name, entity);
    }



}