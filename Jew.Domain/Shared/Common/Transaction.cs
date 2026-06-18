using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Shared.Common;

public abstract record Transaction<TKey, TValue> 
(TKey PartyKey, IEnumerable<TValue> Items, Guid TransactionId,  string? Description, DateTime Date)
:IHasPK<TKey>, IIsTransaction

{
    public abstract decimal TotalAmount{get;}

    [Obsolete("Better use PartyKey")]
    public TKey Key => PartyKey;
}
