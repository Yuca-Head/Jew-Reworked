using System.Threading.Tasks;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;

namespace Jew.Applications.ProductInventory.Commands;


public class InventoryCommands(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public async Task AddProduct(CreateProductDto product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if(string.IsNullOrWhiteSpace(product.Code))
            throw new InventoryException("Debe ingresar un código para crear el producto");
        
        if(string.IsNullOrWhiteSpace(product.CategoryId))
            throw new ProductException("Ingrese una categoría para crear un producto");

        var existsTask = _context.Products.ExistsAsync(product.Code);
        var categoryExistsTask = _context.Categories.ExistsAsync(product.CategoryId);

        await Task.WhenAll(existsTask, categoryExistsTask);

        var exists = await existsTask;
        var categoryExists = await categoryExistsTask;

        if (exists)
            throw new InventoryException("Ya existe un producto con ese código", ProductException.GetFieldName(ProductException.Field.code));

        if (!categoryExists)
            throw new InventoryException($"Categoría {product.CategoryId} no encontrada");

        await _context.Products.AddAsync(
            new Product(product.Code, product.Name, product.CategoryId));

        await _context.Products.SaveChangesAsync();
    }



    public async Task AddCategory(CategoryDto category)
    {
        ArgumentNullException.ThrowIfNull(category);
        
        await _context.Categories.AddAsync(new Category(category.Name, category.Description));
        await _context.Categories.SaveChangesAsync();
    } 





}
