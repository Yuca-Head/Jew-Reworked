using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Persistence.Mappers;
using Jew.Infrastructure.Persistence.Mappers.ProductInventory;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.ProductInventory;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonProductsRepo(Enums.Environment enviroment, string? fileName = null) 
: JsonRepository<string, Product, ProductData>(fileName ?? defaultFileName, new ProductMapper(), enviroment), IProductsRepo
{

    private const string defaultFileName = "Products.json";

    protected override InMemoryProducts InMemoryRepo {get;} = new([]);

    public Product? GetByCode(string code)
    => InMemoryRepo.GetByCode(code);

    public IEnumerable<Product> GetFromCategory(Category category)
    => InMemoryRepo.GetFromCategory(category);

    public IEnumerable<Product> GetFromCategory(string categoryId)
    => InMemoryRepo.GetFromCategory(categoryId);
}