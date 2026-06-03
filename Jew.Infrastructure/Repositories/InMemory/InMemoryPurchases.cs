using Jew.Domain.Purchases.Repositories;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Exceptions;
using Jew.Infrastructure.Repositories.Shared;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemoryPurchases(Dictionary<Guid, Purchase> entities) : 
InMemoryRepository<Purchase, Guid>(entities), IPurchasesRepo
{   

    public override void Add(Purchase entity)
    {
        //Intencional, porque la generación debería ser, en teoría, aleatoria.
        if(!_entities.TryAdd(entity.TransactionId, entity))
            throw new DomainException("Se ha producido un error, el número de transacción ya existe"); 

    }
}