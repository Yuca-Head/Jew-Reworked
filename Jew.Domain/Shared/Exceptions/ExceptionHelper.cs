using System.Numerics;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Sales.Exceptions;

namespace Jew.Domain.Shared.Exceptions;

public enum ExceptionType
{
    Product,
    Domain,
    Category,
    Sale
}

public static class ExceptionHelper
{

    private static Exception GetException(string message, string field, ExceptionType type)
    => type switch
    {
        ExceptionType.Product => new ProductException(message, field),
        ExceptionType.Domain => new DomainException(message, field),
        ExceptionType.Category => new CategoryException(message, field),
        ExceptionType.Sale => new SaleException(message, field),
        _ => new Exception(message){ Source = field}
    };


    #region Campos Vacios
    public static void ThrowIfNull<T,TException>(T value, ExceptionType type, string field = "" )
    {
        if(value == null)
            throw GetException( $"El campo {field} no puede estar vacio", field, type);
    }


    public static void ThrowIfNullOrEmpty(string value, ExceptionType type, string field = "")
    {
        if(string.IsNullOrWhiteSpace(value))
            throw GetException( $"El campo {field} no puede estar vacio", field, type);
    }

    #endregion


    #region Numericos
    public static void ThrowIfLessThan<T>(T value, T otherValue, ExceptionType type, string field = "") where T : INumber<T>
    {
        if(value < otherValue)
            throw GetException( $"El campo {field} debe ser mayor o igual que {otherValue}", field, type);
    }


    public static void ThrowIfGreaterThan<T>(T value, T otherValue, ExceptionType type, string field = "valor") where T : INumber<T>
    {
        if(value > otherValue)
            throw GetException( $"El campo {field} debe ser menor o igual que {otherValue}", field, type);
    }

    public static void ThrowIfLessOrEqual<T>(T value, T otherValue, ExceptionType type, string field = "valor")where T : INumber<T>
    {
        if(value <= otherValue)
            throw GetException($"El campo {field} debe ser mayor que {otherValue}", field, type);
    }

    public static void ThrowIfGreaterOrEqual<T>(T value, T otherValue, ExceptionType type, string field = "valor")where T : INumber<T>
    {
        if(value >= otherValue)
            throw GetException($"El campo {field} debe ser menor que {otherValue}", field, type);
    }
    #endregion
}