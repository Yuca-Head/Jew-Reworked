using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Jew.Domain.Shared.Contacts;

/// <summary>
/// Correo Electrónico.
/// </summary>
public sealed class Email(int id, string correo) : Contacto<MailAddress>(id, Convert(correo))
{

    public override MailAddress Value 
    { 
        get => base.Value; 
        protected set => base.Value = value; 
    }

    public override void Modify(string contacto)
    {
        Value = Convert(contacto);
    }

    private static MailAddress Convert(string value)
    {
        if(MailAddress.TryCreate(value, out var mail))
            return mail;
        else
            throw new ContactException("Formato de correo electronico no válido");
    }


}