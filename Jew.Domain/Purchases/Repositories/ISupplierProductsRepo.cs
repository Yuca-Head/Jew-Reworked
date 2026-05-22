
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Common;

namespace Jew.Domain.Purchases.Repositories;

public interface ISupplierProductsRepo : IRepository<SupplierProduct, SupplierProductPK>
{
    IEnumerable<SupplierProduct> GetBySupplierId(int supplierId);
    IEnumerable<SupplierProduct> GetByProductId(int productId); 


}