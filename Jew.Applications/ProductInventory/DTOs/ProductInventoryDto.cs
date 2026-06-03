namespace Jew.Applications.ProductInventory.DTOs;

public sealed record ProductInventoryDto(ProductWithCategoryDto Product, int Stock, decimal Cost);