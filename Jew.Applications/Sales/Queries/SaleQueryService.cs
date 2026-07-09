using System.Threading.Tasks;
using Jew.Applications.Sales.DTOs;
using Jew.Applications.Sales.DTOs.Sales;
using Jew.Applications.Shared.UnitsOfWork;
using Jew.Domain.Sales.Exceptions;
using Jew.Domain.Sales.Transactions;

namespace Jew.Applications.Sales.Queries;

public sealed class SaleQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public async Task<SaleDto> GetSaleById(Guid id)
    {
        var result = await _context.Sales.GetByIdAsync(id);

        if(result == default)
            throw new SaleException("No se encontró la venta");

        return await Task.FromResult(SaleDto.From(result));
    }
    

    public async Task<IEnumerable<SaleDto>> GetSales()
    =>(await _context.Sales.GetAllAsync()).Select(SaleDto.From);
}   