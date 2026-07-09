using System.Threading.Tasks;
using Jew.Applications.InventoryMovements.Queries;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Mappers;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.ProductInventory.Exceptions;

namespace Jew.Applications.ProductInventory.Queries;

public sealed class InventoryQueryService(CategoryQueryService categoryQuery, ProductQueryService productQuery, StockStateQueryService stockState)
{
    private readonly StockStateQueryService _stockState = stockState;
    private readonly CategoryQueryService _categoryQuery = categoryQuery;
    private readonly ProductQueryService _productQuery = productQuery;




    #region Compounds
    public async Task<ProductWithCategoryDto> GetProductWithCategory(string productId)
    {
        var p = await _productQuery.GetProductByCode(productId);

        return new(p, await _categoryQuery.GetCategoryById(p.CategoryId));
    }

    public async Task<ProductInventoryDto> GetProductInventory(string productCode)
    {
        var p = await _productQuery.GetProductByCode(productCode);
        
        var categoryTask = _categoryQuery.GetCategoryById(p.CategoryId);
        var stockTask = _stockState.GetStock(productCode);
        var costTask = _stockState.GetProductCost(productCode);

        await Task.WhenAll(categoryTask, stockTask, costTask);

        return new(
            new(p, await categoryTask),
            await stockTask,
            await costTask);
    }

    public async Task<IEnumerable<ProductWithCategoryDto>> GetProductsWithCategories()
    {


        var productsTask = _productQuery.GetProducts();
        var categoriesTask = _categoryQuery.GetCategories();

        await Task.WhenAll(productsTask, categoriesTask);

        var products = await productsTask;
        var categories = (await categoriesTask).ToDictionary(c => c.Name);

        return products.Select(p =>
            new ProductWithCategoryDto(
                p,
                categories[p.CategoryId]));
    }

    public async Task<IEnumerable<ProductInventoryDto>> GetProductInventoryDtos()
    {
        List<ProductInventoryDto> result = [];
        foreach(var pAc in await GetProductsWithCategories())
        {
            var code = pAc.ProductDto.Code;
            result.Add(new(pAc, await _stockState.GetStock(code), await _stockState.GetProductCost(code)));   
        }
        return result; 
    }

    public async Task<IEnumerable<ProductInventoryDto>> GetPurchasedProductsOnly()
    {
        var codes = await _stockState.PurchasedOnes();

        return await Task.WhenAll(
            codes.Select(GetProductInventory));
    }

    public async Task<IEnumerable<ProductInventoryDto>> GetAvailableProducts()
    => (await GetPurchasedProductsOnly()).Where(p => p.Product.ProductDto.Active);
    #endregion
}