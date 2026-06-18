using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Domain.InventoryMovements.Entities;

namespace Jew.Avalonia.ViewModels.Movements.Outputs;

public partial class TransactionViewModel: ViewModelBase
{
    public TransactionViewModel(DateTime date, Guid transactionId, string description, 
    string? party, MovementType movementType, IEnumerable<MovementViewModel> movements)
    {
        MovementType  = movementType;
        foreach(var m in movements)
           AddMovement(m);
        
        Date = date;
        Party = party;
        TransactionId = transactionId;
        Description = description;
    }

    public TransactionViewModel(TransactionViewModel model)
    :this(model.Date, model.TransactionId, model.Description, model.Party, model.MovementType, model.Movements)
    {
        
    }

    public void AddMovement(MovementViewModel movement)
    {
        if(movement.MovementType != MovementType) //Aprenda inglés <:c
            throw new ArgumentException("The movements types of the transaction must be the same type as transaction is");
        Movements.Add(movement);
        Amount += movement.Quantity;
        Total += movement.Cost * movement.Quantity;
    }
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TranslatedMovement))]
    private MovementType movementType;
    [ObservableProperty]
    private DateTime _date;
    [ObservableProperty]
    private Guid _transactionId;
    [ObservableProperty]
    private string? _party;
    [ObservableProperty]
    private int _amount;
    [ObservableProperty]
    private decimal _total;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShortDescription))]
    private string _description;
    public string TranslatedMovement => MovementType == MovementType.In ? "Entrada" : "Salida";
    public string ShortDescription =>
    Description.Length > 40 ? Description[..40] + "..." : Description;
    public ObservableCollection<MovementViewModel> Movements {get;} = [];
}   