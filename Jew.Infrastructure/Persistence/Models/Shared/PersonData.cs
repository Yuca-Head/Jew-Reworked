using Jew.Domain.Shared.Keys;

namespace Jew.Infrastructure.Persistence.Models.Shared;

public abstract record PersonData<TKey>(TKey Key, string Name);