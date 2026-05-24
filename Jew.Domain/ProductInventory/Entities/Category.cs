using System.Text.Json.Serialization;
using Jew.Domain.ProductInventory.Exceptions;
using Jew.Domain.Shared.Common;
using Jew.Domain.Shared.Exceptions;
using Jew.Domain.Shared.Keys;



namespace Jew.Domain.ProductInventory.Entities;


public sealed class Category : IHasId, IClonable<Category>
{
    private string name = string.Empty;
    private string description = string.Empty;


    public Category(string name, string? description = null)
    {

        if(string.IsNullOrWhiteSpace(description))
            this.Description = "Sin descripción";
        else
            this.Description = description;
        this.Name = name;
    }

    public int Key {get; private set;}
    internal void SetId(int id)
    {
        if(Key != 0)
            throw new InvalidOperationException("ID ya ha sido asignado.");
        Key = id;
    }
    public string Name 
    { 
        get => name; init
        {
            ExceptionHelper.ThrowIfNullOrEmpty(value, ExceptionType.Category, ProductException.GetFieldName(ProductException.Field.name));   
            name = value.Trim();
        } 
    }

    public string Description 
    { 
        get => description; 
        set
        {
            description = value.Trim();
        }
    }

    public Category Clone()
    => new(this.Name, this.Description){Key = Key, Description = Description};
}
