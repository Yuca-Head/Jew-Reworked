using System.Threading.Tasks;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Queries;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Purchases.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Shared.Keys;

namespace Jew.Applications.Purchases.Queries;

public class SupplierQueryService(IUnitOfWork context, ProductQueryService productQuery)
{
    private readonly IUnitOfWork _context = context;
    private readonly ProductQueryService _productQuery = productQuery;

    #region Singulars
    public async Task<decimal> GetPrice(string productId, CodeKey suplierId)
    {    
        SupplierProduct existingProduct = await _context.SuppliersProducts.GetByIdAsync(new(suplierId, productId))?? 
        throw new SupplierException("Prodcuto no existente");

        return existingProduct.Price;
    }
    public async Task<SupplierDto> GetSupplier(CodeKey id)
    => SupplierDto.From
    (await _context.Suppliers.GetByIdAsync(id) ?? throw new SupplierException("Proveedor no encontrado", nameof(id)));

    private async Task<SupplierProductDto> ConvertSupplierProductToDto(CodeKey supplierId, string productId)
    {   
        var suppTask = GetSupplier(supplierId);

        var prodcutTask = _productQuery.GetProductByCode(productId);

        await Task.WhenAll(suppTask, prodcutTask);

        var supp = await suppTask;
        var prodcut = await prodcutTask;

        return new(supp, prodcut);
    } 

    public Task<bool> SupplierExists(CodeKey id)
    => _context.Suppliers.ExistsAsync(id);

    private Task<SupplierProductDto> ConvertSupplierProductToDto(SupplierProduct product)
    => ConvertSupplierProductToDto(product.SupplierKey, product.ProductId);
    
    #endregion















    #region Collections
    
    public async Task<IEnumerable<SupplierProductDto>> GetAllSupplierProducts()
    => await Task.WhenAll((await _context.SuppliersProducts.GetAllAsync()).Select(ConvertSupplierProductToDto));

    public async Task<IEnumerable<ProductDto>> GetProductsNotSuppliedBy(CodeKey supplierId)
    {
        var ids =(await GetProductsFromSupplier(supplierId)).Select(p => p.Product.Code).ToHashSet();
    
        return (await _context.Products.GetAllAsync())
            .Where(p => !ids.Contains(p.Code)).Select(ProductDto.From);
    }

    
    public async Task<IEnumerable<SupplierProductDto>> GetProductsFromSupplier(CodeKey supplierId)
    =>  await Task.WhenAll((await _context.SuppliersProducts.GetBySupplierIdAsync(supplierId)).Select(ConvertSupplierProductToDto));




    public async Task<IEnumerable<SupplierProductDto>> GetSupplierProducts(CodeKey supplierId)
    {
        var exists = await _context.Suppliers.GetByIdAsync(supplierId) 
        ?? throw new SupplierException($"Proveedor {supplierId} no encontrado", nameof(supplierId));

        return await GetProductsFromSupplier(exists.Key);
    }

    public async Task<IEnumerable<SupplierDto>> GetSupplierByName(string name)
    => (await _context.Suppliers.GetByNameAsync(name))?.Select(SupplierDto.From) 
    ?? throw new SupplierException("Proveedor no econtrado.", nameof(name));

    public async Task<IEnumerable<SupplierDto>> GetSuppliers()
    => (await _context.Suppliers.GetAllAsync()).Select(SupplierDto.From);
    
    #endregion
}