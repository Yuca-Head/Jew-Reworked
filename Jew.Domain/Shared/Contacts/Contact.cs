namespace Jew.Domain.Shared.Contacts;

public abstract class Contacto<T> : IContact where T : notnull
{

    public int Key {get; set;}

    public virtual T Value{get; protected set;}

    public Contacto(int id, T contacto)
    {
        Key = id;
        Value = contacto;
    }

    public string Get()
    => Value.ToString()!;

    public abstract void Modify(string newContact);


    public virtual string Type => GetType().Name;

    public int Id {get; set; }
}