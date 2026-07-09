using Jew.Domain.Sales.Transactions;
using Jew.Domain.Shared.Common;

namespace Jew.Applications.Sales.Repositories;

public interface ISalesRepo : IRepository<Sale, Guid>
{
    
}