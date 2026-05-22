
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories;

public class InMemoryProducts : IProductsRepo
{
    private readonly Dictionary<string, Product> _products = [];

    public InMemoryProducts(bool init = true)
    {
        if(init) 
            Collections.ProductList.ForEach(x => Add(x));
    }
    public void Add(Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if(_products.Values.Any(p => string.Equals(p.Category.Name, entity.Category.Name, StringComparison.OrdinalIgnoreCase) 
        && string.Equals(p.Name, entity.Name, StringComparison.OrdinalIgnoreCase)))
            throw new ProductException("Ya existe este producto", ProductException.Field.name);
        if(Exist(entity.Code))
            throw new ProductException("Ya existe un producto con ese código", ProductException.Field.code);

        _products.Add(entity.Code, entity);
    }

    public IEnumerable<Product> GetAll()
    =>_products.Values;


    public Product? GetByCode(string code)
    => _products.Values.FirstOrDefault(p => string.Equals(p.Code, code, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Product> GetFromCategory(Category category)
    => GetFromCategory(category.Key);

    public IEnumerable<Product> GetFromCategory(int categoryId)
    => [.. _products.Values.Where(p => p.Category.Key == categoryId)];

    public bool Exist(string code)
    => _products.ContainsKey(code);


    public Product? GetById(string code)
    => _products.GetValueOrDefault(code);

}