using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Repositories.InMemory;

namespace Jew.Infrastructure.Repositories.Json;

public abstract class JsonRepository<TValue, TKey>(InMemoryRepository<TValue, TKey> inMemoryRepo, string path)
: IRepository<TValue, TKey> where TValue : IHasPK<TKey> where TKey : notnull
{
    protected readonly InMemoryRepository<TValue, TKey> _inMemoryRepo = inMemoryRepo;
    private readonly string _path = path;
    public string Path => _path;
    protected JsonStorageService<TValue> StorageService { get; } 
    = new JsonStorageService<TValue>(path);
    public virtual void Add(TValue entity)
    => _inMemoryRepo.Add(entity);

    public bool Exist(TKey key)
    => _inMemoryRepo.Exist(key);

    public IEnumerable<TValue> GetAll()
    => _inMemoryRepo.GetAll();
    public TValue? GetById(TKey key)
    => _inMemoryRepo.GetById(key);
    

    public void Load()
    {
        var entities = StorageService.Load();
        foreach (var entity in entities)
            Add(entity);
    }

    public void SaveChanges()
    => StorageService.Save(_inMemoryRepo.GetAll());

    public void Clear()
    {
        _inMemoryRepo.Clear();
    }
}