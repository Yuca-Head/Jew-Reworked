using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Domain.ProductInventory.Repositories;

public interface IProductsRepo : IRepository<Product, string>
{
    IEnumerable<Product> GetFromCategory(Category category);
    IEnumerable<Product> GetFromCategory(int categoryId);
    Product? GetByCode(string code);
    
}