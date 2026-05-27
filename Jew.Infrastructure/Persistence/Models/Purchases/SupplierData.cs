using Jew.Domain.Shared.Contacts;
using Jew.Infrastructure.Persistence.Models.Shared;

namespace Jew.Infrastructure.Persistence.Models.Purchases;

public sealed record SupplierData(int Id, string Name) : PersonData<int>(Id, Name);