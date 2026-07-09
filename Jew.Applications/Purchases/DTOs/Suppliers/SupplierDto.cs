using Jew.Applications.ProductInventory.DTOs;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Shared.Keys;
using Jew.Domain.Shared.People;

namespace Jew.Applications.Purchases.DTOs.Suppliers;

public sealed record SupplierDto(CodeKey CodeKey, string Name) : IParty
{
    public static SupplierDto From(Supplier supplier)
    => new(supplier.Key, supplier.Name);
}