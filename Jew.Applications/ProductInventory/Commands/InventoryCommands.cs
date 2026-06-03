using Jew.Applications.ProductInventory.DTOs;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.ProductInventory.Repositories;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.ProductInventory.Commands;


public class InventoryCommands(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public void AddProduct(CreateProductDto product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if(_context.Products.Exist(product.Code))
            throw new InventoryException("Ya existe un producto con ese código", ProductException.GetFieldName(ProductException.Field.code));

        _context.Products.Add(new(product.Code, product.Name, product.CategoryId));
        _context.Products.SaveChanges();
    }

    public void Activate(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var exists = _context.Products.GetById(product.Code) ??
            throw new InventoryException("Producto no existente");
    
        exists.Activate();
        _context.Products.SaveChanges();
    }


    public void Deactivate(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);


        var exists = _context.Products.GetById(product.Code)??
            throw new InventoryException("Producto no existente");
    
        exists.Deactivate();
        _context.Products.SaveChanges();
    }


    public void AddCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        
        _context.Categories.Add(category);
        _context.Categories.SaveChanges();
    } 





}
