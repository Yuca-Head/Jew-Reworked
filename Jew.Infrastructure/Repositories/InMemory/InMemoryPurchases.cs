using Jew.Applications.Purchases.Repositories;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Exceptions;
using Jew.Infrastructure.Repositories.Shared;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryPurchases(Dictionary<Guid, Purchase> entities) : 
InMemoryRepository<Purchase, Guid>(entities), IPurchasesRepo
{

    protected override DomainException ValidatorException => new("Se ha producido un error, el número de transacción ya existe");

    public override async Task AddAsync(Purchase entity)
    {
        await Validator(entity);
        _entities.Add(entity.TransactionId, entity);
    }

    protected override async Task Validator(params IEnumerable<Purchase> entities)
    {
        foreach(var entity in entities)
        {
            if(await ExistsAsync(entity.TransactionId))
                throw ValidatorException;
        }
    }


    public override async Task AddAsync(IEnumerable<Purchase> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        await Validator(entities);

        foreach(var entity in entities)
            _entities.Add(entity.TransactionId, entity);
    }
}