using Jew.Applications.Sales.DTOs;
using Jew.Domain.Sales.Exceptions;
using Jew.Domain.Sales.Transactions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.Sales.Queries;

public sealed class SaleQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public FullSaleTransactionDto GetSaleById(Guid id)
    {
        var result = _context.Sales.GetById(id);

        if(result == default)
            throw new SaleException("No se encontró la venta");

        return FullSaleTransactionDto.From(result);
    }

    public IEnumerable<FullSaleTransactionDto> GetSales()
    => _context.Sales.GetAll().Select(FullSaleTransactionDto.From);
}   