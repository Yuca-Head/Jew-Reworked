using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.ProductInventory.Repositories;

namespace Jew.Application.ProductInventory;


public class InventoryService(IProductsRepo products, ICategoriesRepo categories)
{
    private readonly IProductsRepo _products = products;

    private readonly ICategoriesRepo _categories = categories;

    public void Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if(_products.Exist(product.Code))
            throw new InventoryException("Ya existe un producto con ese código", ProductException.GetFieldName(ProductException.Field.code));

        _products.Add(product);
    }

    public void Activate(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var exists = _products.GetById(product.Code) ??
            throw new InventoryException("Producto no existente");
    
        exists.Activate();
    }


    public void Deactivate(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);


        var exists = _products.GetById(product.Code)??
            throw new InventoryException("Producto no existente");
    
        exists.Deactivate();
    }


    public void AddCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        
        if(_categories.GetByName(category.Name) is not null)
            throw new InventoryException("Esta categoria ya existe", "nombre");
        
        _categories.Add(category);
    } 

       
    public IReadOnlyList<Product> GetProductFromCategory(Category category)
    => _products.GetFromCategory(category).ToList();
    public IReadOnlyList<Category> GetCategoriesFromProducts(IEnumerable<Product> products)
    => products.GroupBy(p => p.Category.Key).Select(g => g.First().Category).ToList();
    public IReadOnlyList<Product> GetActiveProducts()
    => _products.GetAll().Where(p => p.Active).ToList();

    public Product GetProductByCode(string code)
    => _products.GetByCode(code) ??
    throw new InventoryException("Producto no encontrado");

    public bool ProductExist(string code)
    => _products.Exist(code);

    public Category GetCategoryByName(string name)
    => _categories.GetByName(name) ??
    throw new InventoryException("Categoría no encontrada");

    public IReadOnlyList<Category> GetCategoriesInUse()
    {
        var usedCategoryIds = _products.GetAll()
        .Select(p => p.Category.Key)
        .Distinct()
        .ToHashSet();

        return _categories.GetAll()
            .Where(c => usedCategoryIds.Contains(c.Key))
            .ToList();
    }   

    public IReadOnlyList<Product> GetProducts()
    => _products.GetAll().ToList();

    public IReadOnlyList<Category> GetCategories()
    => _categories.GetAll().ToList();

    public IReadOnlyList<Product> GetProductsFromCategory(Category category)
    => _products.GetFromCategory(category).ToList();





}
