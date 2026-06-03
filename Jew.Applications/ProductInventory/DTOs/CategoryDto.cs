using Jew.Applications.Shared;
using Jew.Domain.ProductInventory.Entities;

namespace Jew.Applications.ProductInventory.DTOs;

public sealed record CategoryDto
(string Name, string Description)
{
    public static CategoryDto From(Category category)
    => new(category.Name, category.Description);
}