namespace Jew.Domain.Shared.Keys;


/// <summary>
/// The entity has a Primary key.
/// </summary>
/// <typeparam name="T">Key type.</typeparam>

public interface IHasPK<T>
{
    public T Key {get; }
    
}