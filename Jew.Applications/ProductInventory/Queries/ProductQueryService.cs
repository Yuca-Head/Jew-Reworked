using System.Threading.Tasks;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Mappers;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.ProductInventory.Exceptions;

namespace Jew.Applications.ProductInventory.Queries;

public sealed class ProductQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;


    public async Task<IEnumerable<ProductDto>> GetProductFromCategory(string categoryId)
    => InventoryMappers.ConvertProductsToDto(await _context.Products.GetFromCategoryAsync(categoryId));

    public async Task<IEnumerable<ProductDto>> GetActiveProducts()
    => InventoryMappers.ConvertProductsToDto((await _context.Products.GetAllAsync()).Where(x => x.Active));

    public async Task<ProductDto> GetProductByCode(string code)
    => ProductDto.From((await _context.Products.GetByCodeAsync(code)) ??
    throw new InventoryException($"Producto {code} no encontrado"));

    public Task<bool> ProductExists(string code)
    => _context.Products.ExistsAsync(code);
    public async Task<IEnumerable<ProductDto>> GetProducts()
    => InventoryMappers.ConvertProductsToDto(await _context.Products.GetAllAsync());




}