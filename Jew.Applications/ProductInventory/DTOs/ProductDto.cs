using AutoMapper;
using Jew.Applications.Shared;
using Jew.Domain.ProductInventory.Entities;

namespace Jew.Applications.ProductInventory.DTOs;

public sealed record ProductDto
(string Code, string Name, string CategoryId, bool Active, DateTime CreatedDate)
{
    public static ProductDto From(Product product)
    => new(product.Code, product.Name, product.CategoryId, product.Active, product.CreatedDate);

}