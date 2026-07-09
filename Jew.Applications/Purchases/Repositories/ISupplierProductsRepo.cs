
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Purchases.Repositories;

public interface ISupplierProductsRepo : IRepository<SupplierProduct, SupplierProductPK>
{
    Task<IEnumerable<SupplierProduct>> GetBySupplierIdAsync(CodeKey supplierId);
    Task<IEnumerable<SupplierProduct>> GetByProductIdAsync(string productId); 


}