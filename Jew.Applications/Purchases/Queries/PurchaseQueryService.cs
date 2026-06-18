using Jew.Applications.Purchases.DTOs.Purchases;
using Jew.Domain.Purchases.Exceptions;
using Jew.Infrastructure.UnitOfWork;

namespace Jew.Applications.Purchases.Queries;

public sealed class PurchaseQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public PurchaseDto GetPurchaseById(Guid id)
    {
        var result = _context.Purchases.GetById(id);
    
        if(result == default)
            throw new PurchaseException("No se encontró la compra");

        return PurchaseDto.From(result);
    }
    

    public IEnumerable<PurchaseDto> GetPurchases()
    => _context.Purchases.GetAll().Select(PurchaseDto.From);
}   