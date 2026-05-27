using Jew.Applications.ProductInventory;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Purchases.Repositories;

namespace Jew.Applications.Purchases;

public class SupplierService(InventoryService inventory, ISuppliersRepo suppliers, ISupplierProductsRepo supplierProducts)
{  
    private readonly ISuppliersRepo _suppliers = suppliers;
    private readonly ISupplierProductsRepo _products = supplierProducts;
    private readonly InventoryService _inventory = inventory;
    public void AddSupplier(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);
        if (_suppliers.Exist(supplier.Key))
            throw new SupplierException("El proveedor ya existe", supplier.Name);

        _suppliers.Add(supplier);
    }
    public IReadOnlyCollection<SupplierProduct> GetProductsFromSupplier(int supplierId)
    => [.._products.GetBySupplierId(supplierId)];

    public Supplier GetSupplier(int id)
    => _suppliers.GetById(id) ?? throw new SupplierException("Proveedor no encontrado", nameof(id));


    public IReadOnlyCollection<SupplierProduct> GetSupplierProducts(int supplierId)
    {
        var exists = _suppliers.GetById(supplierId) ?? throw new SupplierException("Proveedor no encontrado", nameof(supplierId));

        return GetProductsFromSupplier(exists.Key);
    }

    public IEnumerable<Supplier> GetSupplierByName(string name)
    => _suppliers.GetByName(name) ?? throw new SupplierException("Proveedor no econtrado.", nameof(name));

    public IReadOnlyList<Supplier> GetSuppliers()
    => _suppliers.GetAll().ToList();
    


    public void AddProduct(SupplierProduct supplierProduct)
    {
        if(!_suppliers.Exist(supplierProduct.SupplierId))
            throw new SupplierException("Proveedor no encontrado");
        if(!_inventory.ProductExist(supplierProduct.ProductId)) 
            throw new InventoryException("Este producto no existe");
        if(_products.Exist(supplierProduct.Key))
            throw new SupplierException("El proveedor ya tiene este producto");
            
        _products.Add(supplierProduct);
    }

    public void AddProducts(ICollection<SupplierProduct> products)
    {
        foreach(var item in products)
            AddProduct(item);
    }

    public decimal GetPrice(string productId, int suplierId)
    {    
        SupplierProduct existingProduct = _products.GetById(new(suplierId, productId))?? 
        throw new SupplierException("Prodcuto no existente");

        return existingProduct.Price;
    }

    public IReadOnlyList<SupplierProduct> GetSupplierProducts()
    => [.._products.GetAll()]; 

    public IReadOnlyList<Product> GetProductsNotSuppliedBy(int supplierId)
    {
        var ids = GetProductsFromSupplier(supplierId).Select(p => p.ProductId).ToHashSet();
    
        return _inventory.GetProducts()
            .Where(p => !ids.Contains(p.Code))
            .ToList();
    }


}