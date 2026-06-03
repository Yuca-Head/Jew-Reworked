namespace Jew.Applications.Purchases.DTOs.Suppliers;

public sealed record SupplierWithProductsDto(string Supplier, IEnumerable<SupplierDto> Products);