
using Jew.Applications.Sales.Repositories;
using Jew.Domain.Sales.Transactions;
using Jew.Domain.Shared.Exceptions;

namespace Jew.Infrastructure.Repositories.InMemory;

public sealed class InMemorySales(Dictionary<Guid, Sale> entities) : InMemoryRepository<Sale, Guid>(entities), ISalesRepo
{
    protected override DomainException ValidatorException => new("Se ha producido un error, el número de transacción ya existe");

    public override async Task AddAsync(Sale entity)
    {
        await Validator(entity);
        _entities.Add(entity.TransactionId, entity);
    }

    protected override async Task Validator(params IEnumerable<Sale> entities)
    {
        foreach(var entity in entities)
        {
            if(await ExistsAsync(entity.TransactionId))
                throw ValidatorException;
        }
    }
    
    public override async Task AddAsync(IEnumerable<Sale> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        await Validator(entities);

        foreach(var entity in entities)
            _entities.Add(entity.TransactionId, entity);
    }
}