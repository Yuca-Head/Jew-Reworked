using Jew.Applications.Purchases.Repositories;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Persistence.Mappers;
using Jew.Infrastructure.Persistence.Mappers.Purchases;
using Jew.Infrastructure.Persistence.Models;
using Jew.Infrastructure.Persistence.Models.Purchases;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public sealed class JsonSuppliersRepo(Enums.Environment enviroment, string? fileName = null) :
 JsonRepository<CodeKey, Supplier, SupplierData>(fileName ?? defaultFileName, new SupplierMapper(), enviroment), ISuppliersRepo
{
    private const string defaultFileName = "Suppliers.json";

    protected override InMemorySuppliers InMemoryRepo {get;} =  new(new(new CodeKeyComparer()));

    public Task<IEnumerable<Supplier>> GetByNameAsync(string name)
    => InMemoryRepo.GetByNameAsync(name);
}