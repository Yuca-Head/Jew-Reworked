

using Jew.Domain.ProductInventory.Entities;
using Jew.Domain.Sales.Exceptions;

namespace Jew.Domain.Sales.Transactions;

/// <summary>
/// Objeto de paso, es la misma referencia de otro producto solo que con precio.
/// </summary>
public readonly record struct SaleItem
{
    public readonly Product Product{get;}
    public readonly decimal UnitPrice{get;}
    public readonly int Quantity{get;}
    public SaleItem(Product product, decimal unitPrice, int quantity)
    {
        if(unitPrice <= 0)
            throw new SaleException("Ingrese un precio válido mayor que 0", nameof(UnitPrice));
        if(quantity <= 0)
            throw new SaleException("Debe de vender más de un artículo para realizar la operación");   
        Product = product;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}