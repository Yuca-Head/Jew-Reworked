using Jew.Applications.ProductInventory.Repositories;
using Jew.Domain.ProductInventory.Entities;
using Jew.Infrastructure.Persistence.Mappers;
using Jew.Infrastructure.Persistence.Mappers.ProductInventory;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.ProductInventory;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonCategoriesRepo(Enums.Environment environment, string? fileName = null) 
: JsonRepository<string, Category, CategoryData>(fileName ?? defaultFileName, new CategoryMapper(), environment), ICategoriesRepo
{
    protected override InMemoryCategories InMemoryRepo { get; } = new InMemoryCategories();
    private const string defaultFileName = "Categories.json";

}