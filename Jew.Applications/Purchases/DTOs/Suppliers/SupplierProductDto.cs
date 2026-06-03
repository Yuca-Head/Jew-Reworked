using System.Diagnostics.CodeAnalysis;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Domain.Purchases.Entities;

namespace Jew.Applications.Purchases.DTOs.Suppliers;

public sealed record SupplierProductDto(SupplierDto Supplier, ProductDto Product);