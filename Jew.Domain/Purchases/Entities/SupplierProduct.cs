

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

    public SupplierProduct(string productid, CodeKey supplierKey, decimal price)
    {
        ProductId = productid;
        Price = price;
        SupplierKey = supplierKey;
        Key = new(supplierKey, productid);
    }

    public SupplierProduct(SupplierProductPK pk, decimal price) : this(pk.ProductId, pk.SupplierId, price){}
    
    /// <summary>
    /// Supplier Id
    /// </summary>
    public CodeKey SupplierKey { get;}
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



}