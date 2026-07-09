using Jew.Applications.Shared.UnitsOfWork;
using Jew.Infrastructure.Repositories.Json;

namespace Jew.Infrastructure.UnitsOfWork;


public class JsonUnitOfWork(JsonCategoriesRepo categories, JsonProductsRepo products, JsonSuppliersRepo suppliers,
JsonMovementsRepo movements, JsonProductsStockStates stockState, JsonSalesRepo sales, JsonPurchasesRepo purchases,
JsonSupplierProductsRepo supplierProducts, JsonClientsRepo clients) : UnitOfWork(categories, products, suppliers, movements, 
stockState, sales, purchases, supplierProducts, clients)
{
    public JsonUnitOfWork (Enums.Environment environment)
    :
    this(new(environment), new(environment), new(environment),new(environment), new(environment),
    new(environment), new(environment), new(environment), new(environment))
    {}
}