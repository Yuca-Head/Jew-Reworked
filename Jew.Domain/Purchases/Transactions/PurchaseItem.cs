using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Purchases.Exceptions;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Purchases.Transactions;

/// <summary>
/// Objeto de paso, es la misma referencia de otro producto solo que con precio.
/// </summary>
public readonly record struct PurchaseItem
{
    public PurchaseItem(Product product, int quantity, decimal unitCost)
    {
        ArgumentNullException.ThrowIfNull(product); 
        if(quantity < 0)
            throw new PurchaseException("Se agregar al menos un item.", nameof(Quantity));
        if(unitCost < 1)
            throw new PurchaseException("El precio es muy bajo.", nameof(UnitCost));
        Quantity = quantity;
        UnitCost = unitCost;
        Product = product;
    }
    

    public readonly decimal UnitCost{get;}
    public readonly int Quantity{get;}
    public readonly decimal TotalCost => Quantity * UnitCost;

    public readonly Product Product{get;}

}