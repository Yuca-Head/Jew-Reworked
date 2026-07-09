using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Persistence.Models.Purchases;

public sealed record SupplierProductData(SupplierProductPK ProductPK, decimal Price);