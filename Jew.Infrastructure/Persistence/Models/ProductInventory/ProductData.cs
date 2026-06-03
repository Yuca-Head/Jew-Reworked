using Jew.Domain.ProductInventory.Entities;

namespace Jew.Infrastructure.Persistence.Models.ProductInventory;

public sealed record ProductData(string Code, string Name, string CategoryId, bool Active, DateTime CreatedDate);