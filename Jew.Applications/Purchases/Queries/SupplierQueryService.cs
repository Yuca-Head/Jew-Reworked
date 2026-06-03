using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Queries;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.Purchases.Queries;

public class SupplierQueryService(IUnitOfWork context, ProductQueryService productQuery)
{
    private readonly IUnitOfWork _context = context;
    private readonly ProductQueryService _productQuery = productQuery;

    #region Singulars
    public decimal GetPrice(string productId, int suplierId)
    {    
        SupplierProduct existingProduct = _context.SuppliersProducts.GetById(new(suplierId, productId))?? 
        throw new SupplierException("Prodcuto no existente");

        return existingProduct.Price;
    }
    public SupplierDto GetSupplier(int id)
    => SupplierDto.From
    (_context.Suppliers.GetById(id) ?? throw new SupplierException("Proveedor no encontrado", nameof(id)));

    private SupplierProductDto ConvertSupplierProductToDto(int supplierId, string productId)
    {   
        var supp = GetSupplier(supplierId);

        var prodcut = _productQuery.GetProductByCode(productId);

        return new(supp, prodcut);
    } 

    private SupplierProductDto ConvertSupplierProductToDto(SupplierProduct product)
    => ConvertSupplierProductToDto(product.SupplierId, product.ProductId);
    
    #endregion















    #region Collections
    
    public IEnumerable<SupplierProductDto> GetAllSupplierProducts()
    => _context.SuppliersProducts.GetAll().Select(ConvertSupplierProductToDto);

    public IEnumerable<ProductDto> GetProductsNotSuppliedBy(int supplierId)
    {
        var ids = GetProductsFromSupplier(supplierId).Select(p => p.Product.Code).ToHashSet();
    
        return _context.Products.GetAll()
            .Where(p => !ids.Contains(p.Code)).Select(ProductDto.From);
    }

    
    public IEnumerable<SupplierProductDto> GetProductsFromSupplier(int supplierId)
    => _context.SuppliersProducts.GetBySupplierId(supplierId).Select(ConvertSupplierProductToDto);




    public IEnumerable<SupplierProductDto> GetSupplierProducts(int supplierId)
    {
        var exists = _context.Suppliers.GetById(supplierId) 
        ?? throw new SupplierException("Proveedor no encontrado", nameof(supplierId));

        var dto = SupplierDto.From(exists);

        foreach (var item in GetProductsFromSupplier(exists.Key))
            yield return item;
    }

    public IEnumerable<SupplierDto> GetSupplierByName(string name)
    => _context.Suppliers.GetByName(name)?.Select(SupplierDto.From) 
    ?? throw new SupplierException("Proveedor no econtrado.", nameof(name));

    public IEnumerable<Supplier> GetSuppliers()
    => _context.Suppliers.GetAll();
    
    #endregion
}