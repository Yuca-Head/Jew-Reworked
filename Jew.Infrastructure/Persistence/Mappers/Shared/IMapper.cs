namespace Jew.Infrastructure.Persistence.Mappers.Shared;

public interface IMapper<TEntity, TModel>
{
    TEntity ToEntity(TModel data);
    TModel ToModel(TEntity domain);

}