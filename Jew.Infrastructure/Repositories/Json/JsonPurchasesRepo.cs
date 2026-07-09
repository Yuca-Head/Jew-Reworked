
using Jew.Applications.Purchases.Repositories;
using Jew.Domain.Purchases.Transactions;
using Jew.Infrastructure.Persistence.Mappers;
using Jew.Infrastructure.Persistence.Mappers.Purchases;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.Purchases;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonPurchasesRepo
(Enums.Environment environment, string? fileName = null) :
JsonRepository<Guid, Purchase, PurchaseData>(fileName ?? defaultFileName, new PurchaseMapper(), environment), IPurchasesRepo
{   
    private const string defaultFileName = "Purchases.json";
    protected override InMemoryPurchases InMemoryRepo {get;} = new([]);
    
}