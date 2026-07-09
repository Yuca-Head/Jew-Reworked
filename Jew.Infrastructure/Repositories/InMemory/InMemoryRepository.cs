using System.Threading.Tasks;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Exceptions;
using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Repositories.InMemory;

public abstract class InMemoryRepository<TValue, TKey>(Dictionary<TKey, TValue>? entities = null) : 
IRepository<TValue, TKey> where TValue : IHasPK<TKey> where TKey : notnull
{
    protected virtual Dictionary<TKey, TValue> _entities {get;}= entities ?? [];

    public abstract Task AddAsync(TValue entity);

    public abstract Task AddAsync(IEnumerable<TValue> entities);

    public virtual Task<bool> ExistsAsync(TKey key)
    => Task.FromResult(_entities.ContainsKey(key));

    public Task<IEnumerable<TValue>> GetAllAsync()
    => Task.FromResult<IEnumerable<TValue>>(_entities.Values);

    public virtual Task<TValue?> GetByIdAsync(TKey key)
    => Task.FromResult(_entities.GetValueOrDefault(key));

    public Task ClearAsync()
    => Task.FromResult(_entities.Clear);

    public Task LoadAsync()
    => throw new NotImplementedException();

    public Task SaveChangesAsync()
    => throw new NotImplementedException();

    public Task<IEnumerable<TKey>> GetKeysAsync()
    => Task.FromResult<IEnumerable<TKey>>(_entities.Keys);


    protected virtual DomainException ValidatorException {get;} = new();
    protected virtual async Task Validator(params IEnumerable<TValue> entities)
    {
        foreach(var entity in entities)
            if(await ExistsAsync(entity.Key))
                throw ValidatorException;
    }
}