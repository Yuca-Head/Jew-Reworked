using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.InventoryMovements.Repositories;
using Jew.Domain.ProductInventory.Entities;

namespace Jew.Application.InventoryMovements;
public class MovementService(IMovementsRepo movements, IStockStateRepo stockState)
{
    private readonly IMovementsRepo _movements = movements;    
    private readonly IStockStateRepo _stockState = stockState;
    public void AddMovement(InventoryMovement movement)
    {
        _stockState.ApplyMovement(movement);
        _movements.Add(movement);
    }

    public int GetStock(Product product)
    => _stockState.GetById(product.Code)!.Quantity;


    public IReadOnlyList<InventoryMovement> GetMovements()
    => _movements.GetAll().ToList();


    public InventoryMovement GetMovementByID(int iD)
    => _movements.GetById(iD);

    public IReadOnlyList<InventoryMovement> GetMovementsByTransactionId(Guid id)
    => _movements.GetByTransactionId(id).ToList();


    public IReadOnlyList<InventoryMovement> GetMovementsByDate(DateTime date)
    => _movements.GetAll().Where(m => m.Date == date).ToList();

    public IReadOnlyList<Guid> GetTransactionsId()
    => _movements.GetTransactionsId().ToList();

    public decimal GetProductCost(Product product)
    => _stockState.GetOrCreate(product.Code).AverageCost;

}
