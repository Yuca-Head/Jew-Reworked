using Jew.Applications.ProductInventory;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Purchases.Repositories;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.Purchases.Commands;

public class SupplierCommands(IUnitOfWork context)
{  

    private readonly IUnitOfWork _context = context;

    public void AddSupplier(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);
        if (_context.Suppliers.Exist(supplier.Key))
            throw new SupplierException("El proveedor ya existe", supplier.Name);

        _context.Suppliers.Add(supplier);
    }


    public void AddProduct(SupplierProduct supplierProduct)
    {
        if(!_context.Suppliers.Exist(supplierProduct.SupplierId))
            throw new SupplierException("Proveedor no encontrado");
        if(!_context.Products.Exist(supplierProduct.ProductId)) 
            throw new InventoryException("Este producto no existe");
        if(_context.SuppliersProducts.Exist(supplierProduct.Key))
            throw new SupplierException("El proveedor ya tiene este producto");
            
        _context.SuppliersProducts.Add(supplierProduct);
    }

    public void AddProducts(IEnumerable<SupplierProduct> products)
    {
        foreach(var item in products)
            AddProduct(item);
    }
}