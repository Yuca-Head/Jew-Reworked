using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Jew.Domain.Shared.Contacts;


public sealed class Phone(int id, string tel) : Contacto<string>(id, tel)
{


    private string _phone = string.Empty;

    public override string Value
    {
        get => _phone;
        protected set
        {
            if (!Regex.IsMatch(value, @"^\d{4}-\d{4}$"))
                throw new ContactException("Formato telfónico inválido.");
        
            _phone = value;
        }
    }



    public override void Modify(string nuevo)
    => Value = nuevo;
} 