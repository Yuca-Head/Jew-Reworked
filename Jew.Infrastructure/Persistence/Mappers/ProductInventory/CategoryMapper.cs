using Jew.Domain.ProductInventory.Entities;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.ProductInventory;

namespace Jew.Infrastructure.Persistence.Mappers.ProductInventory;

public sealed class CategoryMapper : IMapper<Category, CategoryData>
{
    public Category ToEntity(CategoryData data)
    => new (data.Name, data.Description);

    public CategoryData ToModel(Category domain)
    => new(domain.Name, domain.Description);
}