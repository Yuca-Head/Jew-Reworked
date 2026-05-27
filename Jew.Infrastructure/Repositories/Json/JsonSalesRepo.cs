using Jew.Domain.Sales.Repositories;
using Jew.Domain.Sales.Transactions;
using Jew.Infrastructure.Persistence.Mappers.Sales;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Models.Sales;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonSalesRepo
(Enums.Environment environment, string? fileName = null) 
: JsonRepository<Guid, Sale, SaleData>(fileName ?? defaultFileName, new SaleMapper(), environment), ISalesRepo
{
    private const string defaultFileName = "Sales.json";
    protected override InMemorySales InMemoryRepo {get;} = new([]);
}   