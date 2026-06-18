using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Repositories.InMemory;

public abstract class InMemoryRepository<TValue, TKey>(Dictionary<TKey, TValue>? entities = null) : 
IRepository<TValue, TKey> where TValue : IHasPK<TKey> where TKey : notnull
{
    protected virtual Dictionary<TKey, TValue> _entities {get;}= entities ?? [];

    public abstract void Add(TValue entity);

    public virtual bool Exist(TKey key)
    => _entities.ContainsKey(key);

    public IEnumerable<TValue> GetAll()
    => _entities.Values;

    public TValue? GetById(TKey key)
    => _entities.GetValueOrDefault(key);

    public void Clear()
    => _entities.Clear();

    public void Load()
    => throw new NotImplementedException();

    public void SaveChanges()
    => throw new NotImplementedException();

    public IEnumerable<TKey> GetKeys()
    => _entities.Keys;
}