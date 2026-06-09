using Jew.Applications.ProductInventory;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Purchases.Repositories;
using Jew.Domain.Purchases.Transactions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.Purchases.Commands;

public class SupplierCommands(IUnitOfWork context)
{  

    private readonly IUnitOfWork _context = context;

    public void AddSupplier(SupplierDto supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);
        if (_context.Suppliers.Exist(supplier.Id))
            throw new SupplierException("El proveedor ya existe", supplier.Name);

        _context.Suppliers.Add(new(supplier.Id, supplier.Name));
        _context.Suppliers.SaveChanges();
    }


    public void AddProduct(SupplierProductDto supplierProduct, bool save = true)
    {
        SupplierProductPK key = new(supplierProduct.Supplier.Id, supplierProduct.Product.Code);
        if(!_context.Suppliers.Exist(key.SupplierId))
            throw new SupplierException("Proveedor no encontrado");
        if(!_context.Products.Exist(key.ProductId)) 
            throw new InventoryException("Este producto no existe");
        if(_context.SuppliersProducts.Exist(key))
            throw new SupplierException("El proveedor ya tiene este producto");
            
        _context.SuppliersProducts.Add(new(key.ProductId, key.SupplierId, supplierProduct.Price));
        if(save)
            _context.SuppliersProducts.SaveChanges();
    }

    public void AddProducts(IEnumerable<SupplierProductDto> products)
    {
        foreach(var item in products)
            AddProduct(item, save: false);
        _context.SuppliersProducts.SaveChanges();
    }
}