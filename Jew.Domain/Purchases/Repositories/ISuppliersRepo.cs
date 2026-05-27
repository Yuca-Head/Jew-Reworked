using Jew.Domain.Purchases.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Domain.Purchases.Repositories;

public interface ISuppliersRepo : IRepository<Supplier, int>
{
    IEnumerable<Supplier>? GetByName(string name);
}