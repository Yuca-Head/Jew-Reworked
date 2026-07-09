using Jew.Applications.Purchases.DTOs.Purchases;
using Jew.Domain.Purchases.Exceptions;
using Jew.Applications.Shared.UnitsOfWork;
using System.Threading.Tasks;

namespace Jew.Applications.Purchases.Queries;

public sealed class PurchaseQueryService(IUnitOfWork context)
{
    private readonly IUnitOfWork _context = context;

    public async Task<PurchaseDto> GetPurchaseById(Guid id)
    {
        var result = await _context.Purchases.GetByIdAsync(id);
    
        if(result == default)
            throw new PurchaseException($"No se encontró la compra: {result}");

        return PurchaseDto.From(result);
    }
    

    public async Task<IEnumerable<PurchaseDto>> GetPurchases()
    =>(await _context.Purchases.GetAllAsync()).Select(PurchaseDto.From);
}   