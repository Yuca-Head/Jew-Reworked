using Jew.Applications.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Shared.Common;

public interface IRepository<TValue, TKey>  : IRepository where TValue : IHasPK<TKey>
{
    Task<TValue?> GetByIdAsync(TKey key);
    Task<IEnumerable<TValue>> GetAllAsync();
    Task AddAsync(TValue entity);
    Task AddAsync(IEnumerable<TValue> entities);
    virtual Task RemoveAsync(TKey key){throw new NotSupportedException("No se puede remover este item.");} 
    Task<bool> ExistsAsync(TKey key);
    Task<IEnumerable<TKey>> GetKeysAsync();

}