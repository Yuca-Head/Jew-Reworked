namespace Jew.Infrastructure.Persistence.Models.Purchases;

public sealed record SupplierProductData(string Productid, int SupplierId, decimal Price);