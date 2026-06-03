namespace Jew.Applications.Shared;

public interface IConvertibleDto<TEntity, TDto>
{
    TDto From(TEntity entity);
}