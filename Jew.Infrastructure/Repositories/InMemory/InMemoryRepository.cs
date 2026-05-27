using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Repositories.InMemory;

public abstract class InMemoryRepository<TValue, TKey>(Dictionary<TKey, TValue> entities) : IRepository<TValue, TKey> where TValue : IHasPK<TKey> where TKey : notnull
{
    protected readonly Dictionary<TKey, TValue> _entities = entities;

    public abstract void Add(TValue entity);

    public bool Exist(TKey key)
    => _entities.ContainsKey(key);

    public IEnumerable<TValue> GetAll()
    => _entities.Values;

    public TValue? GetById(TKey key)
    => _entities.GetValueOrDefault(key);

    public void Clear()
    => _entities.Clear();

    public void Init()
    => throw new NotImplementedException();

    public void Load()
    => throw new NotImplementedException();

    public void SaveChanges()
    => throw new NotImplementedException();
}