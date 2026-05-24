using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Common;

namespace Jew.Domain.Purchases.Repositories;

public interface IPurchaseRepo : IRepository<Purchase, Guid>
{
    
}