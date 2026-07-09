using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Purchases.Transactions;

/// <summary>
/// LLave primaria de los productos de un proveedor.
/// </summary>
/// <param name="SupplierId"></param>
/// <param name="ProductId"></param>
public readonly record struct SupplierProductPK(CodeKey SupplierId, string ProductId);