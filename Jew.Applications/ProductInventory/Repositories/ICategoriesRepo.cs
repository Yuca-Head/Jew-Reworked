

using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Applications.ProductInventory.Repositories;

public interface ICategoriesRepo : IRepository<Category, string>
{
}