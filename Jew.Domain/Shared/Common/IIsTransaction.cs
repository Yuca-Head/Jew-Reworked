namespace Jew.Domain.Shared.Common;

public interface IIsTransaction
{
    Guid TransactionId {get;}
    string? Description{get;}
}