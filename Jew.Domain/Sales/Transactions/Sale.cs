using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Sales.Transactions;
public readonly record struct Sale(Client Client, IReadOnlyList<SaleItem> Items, Guid TransactionId, DateTime Date) : IHasPK<Guid>, IIsTransaction
{
    public decimal TotalAmount => Items.Sum(i => i.UnitPrice * i.Quantity);
    
   public Guid Key {get => TransactionId; set => _ = value;}
}

