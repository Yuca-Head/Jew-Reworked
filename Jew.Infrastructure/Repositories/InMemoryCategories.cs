
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.Repositories.Test;


namespace Jew.Infrastructure.Repositories;

public class InMemoryCategories : ICategoriesRepo
{
    private readonly Dictionary<int, Category> _categories = Collections.CategoriesList.ToDictionary(c => c.Key);
    private int idCount = 6;
    public void Add(Category entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var id = idCount++;
        _categories.Add(id, new(entity.Name, entity.Description){Key = id});
    }

    public bool Exist(int key)
    => _categories.ContainsKey(key);

    public IEnumerable<Category> GetAll()
    => _categories.Values.ToList();

    public Category? GetById(int id)
    => _categories.GetValueOrDefault(id);

    public Category? GetByName(string name)
    => _categories.Values.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
}