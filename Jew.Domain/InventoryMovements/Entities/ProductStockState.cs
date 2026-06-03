

using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.InventoryMovements.Entities;

public class ProductStockState : IHasPK<string>, IClonable<ProductStockState>
{
    /// <summary>
    /// Product ID.
    /// This is used as the unique identifier for the stock state of a specific product.
    /// </summary>
    public string Key { get; init;}
    public int Quantity { get; protected set; }
    public decimal AverageCost { get; protected set; }

    public void AddQuantity(int quantity, decimal unitCost)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be a positive value.");

        CalculateAverageCost(unitCost, quantity);

        Quantity += quantity;
    }

    public ProductStockState(Product product, int quantity, decimal averageCost)
    {
        Key = product.Code;
        Quantity = quantity;
        AverageCost = averageCost;
    }

    public ProductStockState(string productId, int quantity, decimal averageCost)
    {
        Key = productId;
        Quantity = quantity;
        AverageCost = averageCost;
    }

    public void ReduceQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be a positive value.");

        if(quantity > Quantity)
            throw new InvalidOperationException("Cannot reduce quantity below zero.");

        Quantity -= quantity;
        if(Quantity == 0)
            AverageCost = 0;
    }


    protected void CalculateAverageCost(decimal unitCost, int quantity)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero.");

        AverageCost = ((AverageCost * Quantity) + (unitCost * quantity)) / (Quantity + quantity);
    }   


    public static ProductStockState CreateDefault(Product product) 
    => new(product, 0, 0);

    public ProductStockState Clone()
    => new(this.Key, this.Quantity, this.AverageCost);
}
