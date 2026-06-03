using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Mappers;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.ProductInventory.Queries;

public sealed class CategoryQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    #region Categories
    public CategoryDto GetCategoryById(string id)
    => CategoryDto.From(_context.Categories.GetById(id) ??
    throw new InventoryException("No se encontró la categoría", nameof(id)));


    public IEnumerable<CategoryDto> GetCategoriesInUse()
    {
        var usedCategoryIds = _context.Products.GetAll()
        .Select(p => p.CategoryId)
        .Distinct()
        .ToHashSet();

        return InventoryMappers.ConvertCategoriesToDto(_context.Categories.GetAll().Where(c => usedCategoryIds.Contains(c.Key)));
    }   

    public IEnumerable<CategoryDto> GetCategories()
    => InventoryMappers.ConvertCategoriesToDto(_context.Categories.GetAll());

    #endregion


}