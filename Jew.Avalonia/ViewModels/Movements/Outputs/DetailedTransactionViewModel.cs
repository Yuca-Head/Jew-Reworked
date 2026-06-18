using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.ProductInventory.DTOs;
using Jew.Domain.InventoryMovements.Entities;

namespace Jew.Avalonia.ViewModels.Movements.Outputs;

public partial class DetailedTransactionViewModel : TransactionViewModel
{
    public DetailedTransactionViewModel
    (DateTime date, Guid transactionId, string description, string? party, MovementType movementType,
    IEnumerable<DetailedMovementViewModel> movements, string? partyName) :
    base(date, transactionId, description, party, movementType, movements.Select(x => x.Movement))
    {
        this.DetailedMovements = [..movements];
        PartyName = partyName;
    }

    
    public ObservableCollection<DetailedMovementViewModel> DetailedMovements {get;} = [];
    [ObservableProperty]
    private string? _partyName;

    public DetailedTransactionViewModel(TransactionViewModel baseModel) : base(baseModel)
    {
        PartyName = null;
    }
}