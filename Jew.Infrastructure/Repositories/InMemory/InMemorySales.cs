using Jew.Domain.Sales.Repositories;
using Jew.Domain.Sales.Transactions;
using Jew.Domain.Shared.Exceptions;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemorySales(Dictionary<Guid, Sale> entities) : InMemoryRepository<Sale, Guid>(entities), ISalesRepo
{
    public override void Add(Sale entity)
    {
        if(!_entities.TryAdd(entity.TransactionId, entity))
            throw new DomainException("Se ha producido un error, el número de transacción ya existe"); 
            //Intencional, porque la generación debería ser, en teoría, aleatoria.
    }
}