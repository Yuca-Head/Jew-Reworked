
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Repositories.Shared;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryCategories(Dictionary<int, Category> entities) : InMemoryRepository<Category, int>(entities), ICategoriesRepo   
{
    private readonly IncrementalKeyGenerator _identity = new(IncrementalKeyGenerator.GetLastKey(entities.Keys));
    public override void Add(Category entity)
    {
        
        ArgumentNullException.ThrowIfNull(entity);
        if(entity.Key == 0)
            entity.SetId(_identity.Next(this));
        _entities.Add(entity.Key, entity);
    }

    public Category? GetByName(string name)
    => _entities.Values.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));


}