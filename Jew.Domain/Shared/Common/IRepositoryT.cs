using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Shared.Common;

public interface IRepository<TValue, TKey>  : IRepository where TValue : IHasPK<TKey>
{
    TValue? GetById(TKey key);
    IEnumerable<TValue> GetAll();
    void Add(TValue entity);
    virtual void Remove(TKey key){throw new NotImplementedException("No se puede remover este item.");} 
    bool Exist(TKey key);


}