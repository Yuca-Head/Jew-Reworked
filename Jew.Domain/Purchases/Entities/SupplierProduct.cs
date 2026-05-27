

using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Purchases.Transactions;
using Jew.Domain.Shared.Exceptions;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.Purchases.Entities;



/// <summary>
/// Esta clase representa la relación entre un proveedor y un producto,
/// incluyendo el precio al que el proveedor vende ese producto.
/// </summary>
public class SupplierProduct : IHasPK<SupplierProductPK>
{
    /// <summary>
    /// Supplier Id
    /// </summary>
    public int SupplierId { get;}
    public string ProductId {get;}
    private decimal price;

    /// <summary>
    /// Solo representa el último precio ingresado del producto.
    /// </summary>
    public decimal Price 
    {
        get => price; set
        {
            ExceptionHelper.ThrowIfLessOrEqual(value, 0, ExceptionType.Product, ProductException.GetFieldName(ProductException.Field.price));
        
            price = value;
        }
    }

    public SupplierProductPK Key {get; init;}

    public SupplierProduct(string productid, int supplierId, decimal price)
    {
        ProductId = productid;
        Price = price;
        SupplierId = supplierId;
        Key = new(supplierId, productid);
    }

}