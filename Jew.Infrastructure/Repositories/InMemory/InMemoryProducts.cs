
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryProducts(Dictionary<string, Product> products) : InMemoryRepository<Product, string>(products), IProductsRepo
{
    public override void Add(Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if(Exist(entity.Code))
            throw new ProductException("Ya existe un producto con ese código", ProductException.Field.code);

        _entities.Add(entity.Code, entity);
    }



    public Product? GetByCode(string code)
    => _entities.Values.FirstOrDefault(p => string.Equals(p.Code, code, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Product> GetFromCategory(Category category)
    => GetFromCategory(category.Name);

    public IEnumerable<Product> GetFromCategory(string categoryId)
    => [.. _entities.Values.Where(p => p.CategoryId == categoryId)];


}