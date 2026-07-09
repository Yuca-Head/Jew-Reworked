using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Applications.ProductInventory.Repositories;

public interface IProductsRepo : IRepository<Product, string>
{
    Task<IEnumerable<Product>> GetFromCategoryAsync(string categoryId);
    Task<Product?> GetByCodeAsync(string code);
    
}