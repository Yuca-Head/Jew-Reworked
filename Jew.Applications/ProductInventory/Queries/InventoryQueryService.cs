using Jew.Applications.InventoryMovements.Queries;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Mappers;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.ProductInventory.Queries;

public sealed class InventoryQueryService(CategoryQueryService categoryQuery, ProductQueryService productQuery, StockStateQueryService stockState)
{
    private readonly StockStateQueryService _stockState = stockState;
    private readonly CategoryQueryService _categoryQuery = categoryQuery;
    private readonly ProductQueryService _productQuery = productQuery;




    #region Compounds
    public ProductWithCategoryDto GetProductWithCategory(string productId)
    {
        var p = _productQuery.GetProductByCode(productId);

        return new(p, _categoryQuery.GetCategoryById(p.CategoryId));
    }

    public ProductInventoryDto GetProductInventory(string productCode)
    {
        var p = _productQuery.GetProductByCode(productCode);
        return new(new(p, _categoryQuery.GetCategoryById(p.CategoryId)),
        _stockState.GetStock(productCode), _stockState.GetProductCost(productCode));
    }

    public IEnumerable<ProductWithCategoryDto> GetProductsWithCategories()
    {
        /*
        Se mantiene si no se encuentra el formato querido.
        foreach(var cat in _categoryQuery.GetCategories())
            foreach (var pr in _productQuery.GetProductFromCategory(cat.Name))
                yield return new(pr, cat);  
        */

        foreach(var product in _productQuery.GetProducts())
            yield return new(product, _categoryQuery.GetCategoryById(product.CategoryId));
    }

    public IEnumerable<ProductInventoryDto> GetProductInventoryDtos()
    {
        foreach(var pAc in GetProductsWithCategories())
        {
            var code = pAc.ProductDto.Code;
            yield return new(pAc, _stockState.GetStock(code), _stockState.GetProductCost(code));
            
        }
    }

    public IEnumerable<ProductInventoryDto> GetPurchasedProductsOnly()
    => _stockState.PurchasedOnes().Select(GetProductInventory);

    public IEnumerable<ProductInventoryDto> GetAvailableProducts()
    => GetPurchasedProductsOnly().Where(p => p.Product.ProductDto.Active);
    #endregion
}