using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Persistence.Models.Shared;

namespace Jew.Infrastructure.Persistence.Models.Sales;

public sealed record ClientData(CodeKey Key, string Name) : PersonData<CodeKey>(Key, Name);