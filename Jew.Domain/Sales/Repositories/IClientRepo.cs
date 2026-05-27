

using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Sales.Repositories;

public interface IClientRepo : IRepository<Client, CodeKey>
{
    IEnumerable<Client>? GetByName(string name);
}