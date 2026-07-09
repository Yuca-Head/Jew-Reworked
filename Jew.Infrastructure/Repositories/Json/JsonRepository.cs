using System.Threading.Tasks;
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
    public virtual Task AddAsync(TEntity entity)
    => InMemoryRepo.AddAsync(entity);
    public virtual Task<bool> ExistsAsync(TKey key)
    => InMemoryRepo.ExistsAsync(key);
    public virtual Task<IEnumerable<TEntity>> GetAllAsync()
    => InMemoryRepo.GetAllAsync();
    public virtual Task<TEntity?> GetByIdAsync(TKey key)
    => InMemoryRepo.GetByIdAsync(key);
    protected IMapper<TEntity, TModel> Mapper { get; }
    public virtual async Task LoadAsync()
    {
        await ClearCacheAsync();

        var models = await StorageService.LoadAsync();

        foreach (var model in models)
            await AddAsync(Mapper.ToEntity(model));
    }
    public virtual async Task SaveChangesAsync()
    {
        var models = (await GetAllAsync())
        .Select(Mapper.ToModel);
        
        await StorageService.SaveAsync(models);
    }
    public virtual Task ClearCacheAsync()
    {
        InMemoryRepo.ClearAsync();
        return Task.CompletedTask;
    }

    public virtual void ClearMemory()
    {
        StorageService.Clear();
        ClearCacheAsync();
    }


    public Task AddAsync(IEnumerable<TEntity> entities)
    => InMemoryRepo.AddAsync(entities);

    public Task<IEnumerable<TKey>> GetKeysAsync()
    => InMemoryRepo.GetKeysAsync();
}