using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Sales.Transactions;
public readonly record struct Sale(CodeKey ClientKey, IReadOnlyList<SaleItem> Items, Guid TransactionId,  string? Description, DateTime Date) 
: IHasPK<Guid>
{
    public decimal TotalAmount => Items.Sum(i => i.UnitPrice * i.Quantity);
    
   public Guid Key {get => TransactionId; set => _ = value;}
}

