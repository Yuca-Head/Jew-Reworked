using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.ProductInventory.Mappers;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.ProductInventory.Queries;

public sealed class ProductQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;


    public IEnumerable<ProductDto> GetProductFromCategory(string categoryId)
    => InventoryMappers.ConvertProductsToDto(_context.Products.GetFromCategory(categoryId));

    public IEnumerable<ProductDto> GetActiveProducts()
    => InventoryMappers.ConvertProductsToDto( _context.Products.GetAll().Where(x => x.Active));

    public ProductDto GetProductByCode(string code)
    => ProductDto.From(_context.Products.GetByCode(code) ??
    throw new InventoryException($"Producto {code} no encontrado"));

    public bool ProductExists(string code)
    => _context.Products.Exist(code);
    public IEnumerable<ProductDto> GetProducts()
    => InventoryMappers.ConvertProductsToDto(_context.Products.GetAll());




}