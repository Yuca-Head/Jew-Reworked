
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Repositories.Shared;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryCategories() 
: InMemoryRepository<Category, string>(new Dictionary<string, Category>(StringComparer.OrdinalIgnoreCase)), ICategoriesRepo   
{

    public override void Add(Category entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        if(Exist(entity.Name))
            throw new InventoryException($"Ya existe ya existe la categoria {entity.Name}", nameof(entity.Name));

        _entities.Add(entity.Name, entity);
    }
    


}