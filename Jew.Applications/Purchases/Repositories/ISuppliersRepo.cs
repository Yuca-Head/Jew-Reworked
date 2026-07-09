using Jew.Domain.Purchases.Entities;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Purchases.Repositories;

public interface ISuppliersRepo : IRepository<Supplier, CodeKey>
{
    Task<IEnumerable<Supplier>> GetByNameAsync(string name);
}