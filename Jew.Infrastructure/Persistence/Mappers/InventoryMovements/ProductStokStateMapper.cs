using Jew.Domain.InventoryMovements.Entities;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.InventoryMovements;


namespace Jew.Infrastructure.Persistence.Mappers.InventoryMovements;

public sealed class ProductStockStateMapper() : IMapper<ProductStockState, ProductStockStateData>
{
    public ProductStockState ToEntity(ProductStockStateData data)
    => new(data.ProductId, data.Quantity, data.AverageCost);
    public ProductStockStateData ToModel(ProductStockState domain)
    => new(domain.Key, domain.Quantity, domain.AverageCost);
}