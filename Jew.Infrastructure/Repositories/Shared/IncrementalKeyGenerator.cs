using System.Threading.Tasks;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Repositories.Shared;

public sealed class IncrementalKeyGenerator(int initialKey = 0)
{
    public int CurrentKey { get; private set; } = initialKey;


    public async Task<int> Next<T>(IRepository<T, int> repository) where T : IHasPK<int>
    {
        CurrentKey++;
        while(await repository.ExistsAsync(CurrentKey))
            CurrentKey++;
        return CurrentKey;
    }

    public int UpdateToLast(IEnumerable<int> keys)
    {
        if(!keys.Any())
            return CurrentKey;

        int maxKey = keys.Max();
        if (maxKey > CurrentKey)
            CurrentKey = maxKey;
        

        return CurrentKey;
    }

    public static int GetLastKey(IEnumerable<int> keys)
    => keys.Any() ? keys.Max() : 0;
}