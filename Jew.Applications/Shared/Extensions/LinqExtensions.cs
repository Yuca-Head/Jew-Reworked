namespace Jew.Applications.Shared.Extensions;

public static class LinqExtensions
{
    public static async Task<TResult> SelectAsync<T, TResult>(
    this Task<T> task,
    Func<T, TResult> selector)
    => selector(await task);

}