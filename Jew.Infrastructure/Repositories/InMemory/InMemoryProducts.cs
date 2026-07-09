
using System.Threading.Tasks;
using Jew.Applications.ProductInventory.Repositories;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Shared.Exceptions;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryProducts(Dictionary<string, Product> products) :
 InMemoryRepository<Product, string>(products), IProductsRepo
{

    protected override ProductException ValidatorException => 
    new("Ya existe un producto con ese código", ProductException.Field.code);
    public override async Task AddAsync(Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await Validator(entity);

        _entities.Add(entity.Code, entity);
    }

    public override async Task AddAsync(IEnumerable<Product> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        await Validator(entities);

        foreach(var entity in entities)
            _entities.Add(entity.Code, entity);
    }

    public Task<Product?> GetByCodeAsync(string code)
    => Task.FromResult(_entities.Values.FirstOrDefault(p => string.Equals(p.Code, code, StringComparison.OrdinalIgnoreCase)));


    public Task<IEnumerable<Product>> GetFromCategoryAsync(string categoryId)
    => Task.FromResult<IEnumerable<Product>>([.. _entities.Values.Where(p => p.CategoryId == categoryId)]);
}