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
    public ProductWithCategoryDto ToProductWithCategory(string productId, string categoryId)
    => new(_productQuery.GetProductByCode(productId), _categoryQuery.GetCategoryById(categoryId));

    public ProductInventoryDto ToProductInventory(string productCode, string categoryId)
    => 
    new(new(_productQuery.GetProductByCode(productCode), _categoryQuery.GetCategoryById(categoryId)),
    _stockState.GetStock(productCode), _stockState.GetProductCost(productCode));

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
    #endregion
}