using Jew.Domain.Shared.Contacts;
using Jew.Domain.Shared.Keys;
using Jew.Infrastructure.Persistence.Models.Shared;

namespace Jew.Infrastructure.Persistence.Models.Purchases;

public sealed record SupplierData(CodeKey Key, string Name) : PersonData<CodeKey>(Key, Name);