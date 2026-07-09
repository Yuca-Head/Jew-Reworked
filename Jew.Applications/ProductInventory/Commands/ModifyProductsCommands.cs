using Jew.Applications.InventoryMovements.Queries;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.ProductInventory.Exceptions;

namespace Jew.Applications.ProductInventory.Commands;

public sealed class ModifyProductsCommands(IUnitOfWork context, StockStateQueryService stockState)
{
    private readonly IUnitOfWork _context = context;
    private readonly StockStateQueryService _stockState = stockState;

    public async Task ModifyProduct(ModifyProductDto productDto)
    {
        var existing = await _context.Products.GetByIdAsync(productDto.Code)??
            throw new InventoryException("Producto no existente");
        var state = (await _stockState.GetProductState(existing.Code))!;
        
        if(productDto.NewState is not null && productDto.NewState.Value != existing.Active)
            if(productDto.NewState == true)
                existing.Activate();
            else    
            {
                if(state.Quantity != 0)
                    throw new InventoryException("Solo se puede desactivar un producto que no posee existencias");
                existing.Deactivate();  
            }
        
        if(productDto.NewName is not null && productDto.NewName != existing.Name)
            existing.Name = productDto.NewName;

        await _context.Products.SaveChangesAsync();
    }

}