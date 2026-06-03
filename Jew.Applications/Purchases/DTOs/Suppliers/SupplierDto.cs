using Jew.Applications.ProductInventory.DTOs;
using Jew.Domain.Purchases.Entities;

namespace Jew.Applications.Purchases.DTOs.Suppliers;

public sealed record SupplierDto(int Id, string Name)
{
    public static SupplierDto From(Supplier supplier)
    => new(supplier.Key, supplier.Name);
}