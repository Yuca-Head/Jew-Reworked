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

    private string categoryId;

    public DateTime CreatedDate
    {get; init;} 


    /// <summary>
    /// JsonConstructor. No usar para crear productos.
    /// </summary>
    [JsonConstructor]
    public Product(){}
    public Product(string code, string name, string categoryId)
    {
        Code = code;
        Name = name;
        CreatedDate = DateTime.Now;
        active = true;
        CategoryId = categoryId;
    }

    public Product(string code, string name, Category category):
    this(code, name, category.Name)
    {
        
    }

    //PARA NO TOMAR LA REFERENCIA DIRECTA
    public Product(Product original)
    {
        Code = original.Code;
        Name = original.Name;
        Active = original.Active;
        CategoryId = original.CategoryId;
        CreatedDate = original.CreatedDate;
    }

    internal Product(string code, string name, string categoryId, bool active, DateTime creation) :
    this (code, name, categoryId)
    {
        this.Active = active;
        this.CreatedDate = creation;
    }

    public Product Clone()
    => new(this);
    
    [Obsolete("Mejor usar código")]
    /// <summary>
    /// Mismo que código.
    /// </summary>
    public string  Key {get => Code;}

    public string Code
    {
        get => code;
        init
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ProductException("Debe ingresar un código para el producto");

            code = value.Trim();
        }

    }
    public string Name
    {
        get => name;
        set
        {

            if(string.IsNullOrWhiteSpace(value))
                throw new ProductException("Debe ingresar un nombre para el producto");
            if(!value.Any(char.IsLetter))
                throw new ProductException("El nombre del producto debe contener al menos una letra", ProductException.Field.name);
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


    public string CategoryId
    {
        get => categoryId;
        set
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ProductException("Ingrese una categoría para el producto");
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            categoryId = value;
        }
    }

    public override bool Equals(object? obj)  
    => obj is Product p && p.Code == Code;      


    public override int GetHashCode()  
    => Code.GetHashCode();  


}