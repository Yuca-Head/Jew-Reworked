using Jew.Domain.ProductInventory.Entities;

namespace Jew.Applications.ProductInventory.DTOs;

public sealed record CreateProductDto
(string Code, string Name, string CategoryId);