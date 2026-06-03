
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Repositories.Shared;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryCategories(Dictionary<string, Category> entities) : InMemoryRepository<Category, string>(entities), ICategoriesRepo   
{

    public override void Add(Category entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        if(!_entities.TryAdd(entity.Name, entity))
            throw new InventoryException("Ya existe ya existe esa categoria", nameof(entity.Name));
    }


}