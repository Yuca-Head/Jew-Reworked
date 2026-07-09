using Jew.Applications.ProductInventory.Repositories;
using Jew.Domain.ProductInventory.Entities;
using Jew.Infrastructure.Persistence.Mappers;
using Jew.Infrastructure.Persistence.Mappers.ProductInventory;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.ProductInventory;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonProductsRepo(Enums.Environment enviroment, string? fileName = null) 
: JsonRepository<string, Product, ProductData>(fileName ?? defaultFileName, new ProductMapper(), enviroment),
IProductsRepo
{

    private const string defaultFileName = "Products.json";

    protected override InMemoryProducts InMemoryRepo {get;} = new(new(StringComparer.OrdinalIgnoreCase));

    public Task<Product?> GetByCodeAsync(string code)
    => InMemoryRepo.GetByCodeAsync(code);


    public Task<IEnumerable<Product>> GetFromCategoryAsync(string categoryId)
    => InMemoryRepo.GetFromCategoryAsync(categoryId);
}