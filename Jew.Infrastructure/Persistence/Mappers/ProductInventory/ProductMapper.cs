using Jew.Domain.ProductInventory.Entities;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.ProductInventory;

namespace Jew.Infrastructure.Persistence.Mappers.ProductInventory;

public sealed class ProductMapper : IMapper<Product, ProductData>
{
    public Product ToEntity(ProductData data)
    => new(data.Code, data.Name, data.Category, data.Active, data.CreatedDate);

    public ProductData ToModel(Product domain)
    => new(domain.Code, domain.Name, domain.Category, domain.Active, domain.CreatedDate);
}