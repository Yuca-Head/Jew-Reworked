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

        if(string.IsNullOrWhiteSpace(product.Code))
            throw new InventoryException("Debe ingresar un código para crear el producto");

        if(_context.Products.Exist(product.Code))
            throw new InventoryException("Ya existe un producto con ese código", ProductException.GetFieldName(ProductException.Field.code));

        _context.Products.Add(new(product.Code, product.Name, product.CategoryId));
        _context.Products.SaveChanges();
    }

    public void ModifyProduct(ModifyProductDto productDto)
    {
        var existing = _context.Products.GetById(productDto.Code)??
            throw new InventoryException("Producto no existente");
        
        if(productDto.NewState is not null)
            if(productDto.NewState == true)
                existing.Activate();
            else    
                existing.Deactivate();
        
        if(productDto.NewName is not null)
            existing.Name = productDto.NewName;
    }


    public void AddCategory(CategoryDto category)
    {
        ArgumentNullException.ThrowIfNull(category);
        
        _context.Categories.Add(new(category.Name, category.Description));
        _context.Categories.SaveChanges();
    } 





}
