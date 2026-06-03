using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Shared.Common;

namespace Jew.Domain.ProductInventory.Repositories;

public interface IProductsRepo : IRepository<Product, string>
{
    IEnumerable<Product> GetFromCategory(string categoryId);
    Product? GetByCode(string code);
    
}