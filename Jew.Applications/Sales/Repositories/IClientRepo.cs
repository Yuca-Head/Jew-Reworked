

using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Sales.Repositories;

public interface IClientRepo : IRepository<Client, CodeKey>
{
    Task<IEnumerable<Client>> GetByNameAsync(string name);
}