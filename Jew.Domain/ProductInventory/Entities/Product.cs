using System.Text.Json.Serialization;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Exceptions;
using Jew.Domain.Shared.Keys;

namespace Jew.Domain.ProductInventory.Entities;

public sealed class Product : IHasPK<string>, IClonable<Product>
{

    private string code;
    private string name;

    private bool active;

    private Category category;

    public DateTime CreatedDate
    {get;} 


    /// <summary>
    /// JsonConstructor. No usar para crear productos.
    /// </summary>
    [JsonConstructor]
    public Product(){}
    public Product(string code, string name, Category category)
    {
        Code = code;
        Name = name;
        CreatedDate = DateTime.Now;
        active = true;
        Category = category;
    }

    //PARA NO TOMAR LA REFERENCIA DIRECTA
    public Product(Product original)
    {
        Code = original.Code;
        Name = original.Name;
        Active = original.Active;
        Category = original.Category;
        CreatedDate = original.CreatedDate;
    }

    public Product Clone()
    => new(this);
    
    [Obsolete("Mejor usar código")]
    /// <summary>
    /// Mismo que código.
    /// </summary>
    public string  Key {get => Code; set => _ = value;}

    public string Code
    {
        get => code;
        init
        {
            ExceptionHelper.ThrowIfNullOrEmpty(value, ExceptionType.Product, ProductException.GetFieldName(ProductException.Field.code));

            code = value.Trim();
        }

    }
    public string Name
    {
        get => name;
        set
        {
            ExceptionHelper.ThrowIfNullOrEmpty(value, ExceptionType.Product, ProductException.GetFieldName(ProductException.Field.name));

            if(!value.Any(char.IsLetter))
                throw new ProductException("El nombre debe contener al menos una letra", ProductException.Field.name);
            name = value.Trim();

        }
    }

    public bool Active
    {
        get => active;

        private set
        {
            active = value;
        } 
    }

    public void Activate()
    {
        if(Active)
            throw new ProductException("Esto producto ya está activo", ProductException.Field.state);

        Active = true;
    }

    public void Deactivate()
    {
    
        if(!Active)
            throw new ProductException("Este producto ya está inactivo", ProductException.Field.state);

        Active = false;
    }


    public Category Category
    {
        get => category;
        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(category));
            category = value;
        }
    }

    public override bool Equals(object? obj)  
    => obj is Product p && p.Code == Code;  


    public override int GetHashCode()  
    => Code.GetHashCode();  


}