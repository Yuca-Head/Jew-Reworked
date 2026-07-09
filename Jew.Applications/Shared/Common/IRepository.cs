namespace Jew.Applications.Shared.Common;

public interface IRepository
{
    Task LoadAsync();
    Task SaveChangesAsync();
    
}