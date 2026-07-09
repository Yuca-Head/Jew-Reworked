using Jew.Applications.ProductInventory.DTOs;
using Jew.Domain.ProductInventory.Entities;

namespace Jew.Applications.ProductInventory.Mappers;

public static class InventoryMappers
{

    public static IEnumerable<ProductDto> ConvertProductsToDto(IEnumerable<Product> products)
    => products.Select(ProductDto.From);

    public static IEnumerable<CategoryDto> ConvertCategoriesToDto(IEnumerable<Category> categories)
    => categories.Select(CategoryDto.From);
}