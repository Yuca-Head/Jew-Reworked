using System.Threading.Tasks;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Mappers;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.ProductInventory.Exceptions;

namespace Jew.Applications.ProductInventory.Queries;

public sealed class CategoryQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    #region Categories
    public async Task<CategoryDto> GetCategoryById(string id)
    => CategoryDto.From(await _context.Categories.GetByIdAsync(id) ??
    throw new InventoryException($"No se encontró la categoría: {id}", nameof(id)));


    public async Task<IEnumerable<CategoryDto>> GetCategoriesInUse()
    {
        var usedCategoryIds = (await _context.Products.GetAllAsync())
        .Select(p => p.CategoryId)
        .Distinct()
        .ToHashSet();

        return InventoryMappers.ConvertCategoriesToDto((await _context.Categories.GetAllAsync())
        .Where(c => usedCategoryIds.Contains(c.Key)));
    }   

    public async Task<IEnumerable<CategoryDto>> GetCategories()
    => InventoryMappers.ConvertCategoriesToDto(await _context.Categories.GetAllAsync());

    #endregion


}