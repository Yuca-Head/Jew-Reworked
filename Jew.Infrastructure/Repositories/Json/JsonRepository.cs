using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Enums;
using Jew.Infrastructure.Persistence.Mappers;
using Jew.Infrastructure.Persistence.Mappers.Shared;
using Jew.Infrastructure.Persistence.Serialization;
using Jew.Infrastructure.Repositories.InMemory;


namespace Jew.Infrastructure.Repositories.Json;



public abstract class JsonRepository<TKey, TEntity, TModel>
: IRepository<TEntity, TKey> where TEntity : IHasPK<TKey> where TKey : notnull 
{
    public JsonRepository(string fileName, IMapper<TEntity, TModel> mapper, Enums.Environment environment)
    {
        EnvironmentType = environment;
        FilePath = AppPaths.GetDataFilePath(environment, fileName);
        StorageService = new(FilePath);
        Mapper = mapper;
    }

    protected abstract InMemoryRepository<TEntity, TKey> InMemoryRepo {get;}
    protected readonly string FilePath;
    public Enums.Environment EnvironmentType {get;}

    protected JsonStorageService<TModel> StorageService { get; }
    public virtual void Add(TEntity entity)
    => InMemoryRepo.Add(entity);
    public virtual bool Exist(TKey key)
    => InMemoryRepo.Exist(key);
    public virtual IEnumerable<TEntity> GetAll()
    => InMemoryRepo.GetAll();
    public virtual TEntity? GetById(TKey key)
    => InMemoryRepo.GetById(key);
    protected IMapper<TEntity, TModel> Mapper { get; }
    public virtual void Load()
    {
        ClearCache();
        var models = StorageService.Load();
        foreach (var model in models)
            Add(Mapper.ToEntity(model));
    }
    public virtual void SaveChanges()
    {
        var models = GetAll()
        .Select(Mapper.ToModel);
        
        StorageService.Save(models);
    }
    public virtual void ClearCache()
    {
        InMemoryRepo.Clear();
    }

    public virtual void ClearMemory()
    {
        StorageService.Clear();
        ClearCache();
    }

    public virtual IEnumerable<TKey> GetKeys()
    => InMemoryRepo.GetKeys();
}