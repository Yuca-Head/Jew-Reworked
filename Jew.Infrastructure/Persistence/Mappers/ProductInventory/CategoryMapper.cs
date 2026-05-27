using Jew.Domain.ProductInventory.Entities;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.ProductInventory;

namespace Jew.Infrastructure.Persistence.Mappers.ProductInventory;

public sealed class CategoryMapper : IMapper<Category, CategoryData>
{
    public Category ToEntity(CategoryData data)
    {
        var result = new Category(data.Name, data.Description);
        result.SetId(data.Key);
        return result;
    }

    public CategoryData ToModel(Category domain)
    => new(domain.Key, domain.Name, domain.Description);
}