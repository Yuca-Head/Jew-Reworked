namespace Jew.Domain.Shared.Common;

public interface IRepository
{
    void Load();
    void SaveChanges();
}