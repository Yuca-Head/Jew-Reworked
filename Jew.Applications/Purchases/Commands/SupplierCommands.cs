using System.Threading.Tasks;
using Jew.Applications.ProductInventory;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Purchases.Transactions;

namespace Jew.Applications.Purchases.Commands;

public class SupplierCommands(IUnitOfWork context)
{  

    private readonly IUnitOfWork _context = context;

    public async Task AddSupplier(SupplierDto supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        if (await _context.Suppliers.ExistsAsync(supplier.CodeKey))
            throw new SupplierException("El proveedor ya existe", supplier.Name);

        await _context.Suppliers.AddAsync(new Supplier(supplier.CodeKey, supplier.Name));
        await _context.Suppliers.SaveChangesAsync();
    }


    public async Task AddProduct(SupplierProductDto supplierProduct)
    {
        var key = await ProductValidatorAsync(supplierProduct);
            
        await _context.SuppliersProducts.AddAsync(new SupplierProduct(key.ProductId, key.SupplierId, supplierProduct.Price));
        
        await _context.SuppliersProducts.SaveChangesAsync();
    }

    public async Task AddProducts(IEnumerable<SupplierProductDto> supplierProducts)
    {
        var tasks = supplierProducts.Select(async dto =>
        {
            var key = await ProductValidatorAsync(dto);

            return new SupplierProduct(
                key.ProductId,
                key.SupplierId,
                dto.Price);
        });

        var entities = await Task.WhenAll(tasks);

        await _context.SuppliersProducts.AddAsync(entities);
        await _context.SuppliersProducts.SaveChangesAsync();
    }

    private async Task<SupplierProductPK> ProductValidatorAsync(SupplierProductDto supplierProduct)
    {
        SupplierProductPK key = new(supplierProduct.Supplier.CodeKey, supplierProduct.Product.Code);

        var supplierExistsTask = _context.Suppliers.ExistsAsync(key.SupplierId);
        var productExistsTask = _context.Products.ExistsAsync(key.ProductId);
        var supplierProductExistsTask = _context.SuppliersProducts.ExistsAsync(key);
        
        await Task.WhenAll(supplierExistsTask, productExistsTask, supplierProductExistsTask);

        var supplierExists = await supplierExistsTask;
        var productExists = await productExistsTask;
        var supplierProductExists = await supplierProductExistsTask;
        if(!supplierExists)
            throw new SupplierException("Proveedor no encontrado");
        if(!productExists) 
            throw new InventoryException("Este producto no existe");
        if(supplierProductExists)
            throw new SupplierException("El proveedor ya tiene este producto");
        
        return key;
    }
}