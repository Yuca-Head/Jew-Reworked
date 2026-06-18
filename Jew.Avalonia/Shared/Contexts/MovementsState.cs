using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Messaging;
using Jew.Applications.InventoryMovements.DTOs;
using Jew.Applications.InventoryMovements.Queries;
using Jew.Applications.Purchases.Queries;
using Jew.Applications.Sales.Queries;
using Jew.Avalonia.Messaging;
using Jew.Avalonia.ViewModels.Movements.Mappers;
using Jew.Avalonia.ViewModels.Movements.Outputs;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Purchases.Transactions;

namespace Jew.Avalonia.Shared.Contexts;

public sealed class MovementsState
{
    private readonly MovementVM_Mapper _mapper = new();
    public enum Filters
    {
        None,
        Sales,
        Purchases,
    }

    private static Func<MovementViewModel, bool> Selector(Filters filter)
    => (x) => filter switch
    {
        Filters.Purchases => x.MovementType == MovementType.In,
        Filters.Sales => x.MovementType == MovementType.Out,
        _ => true
    };

    public MovementsState(MovementQueryService movementQuery, SaleQueryService saleQuery, PurchaseQueryService purchaseQuery)
    {
        _movementQuery = movementQuery;
        _saleQuery = saleQuery;
        _purchaseQuery = purchaseQuery;
        WeakReferenceMessenger.Default.Register<MovementUpdateMessage>(this,
        (_, e) =>
        {
            Update(e.TransactionsKey);
        }
        );

        Update(null);
    }

    private MovementQueryService _movementQuery;
    private SaleQueryService _saleQuery;
    private PurchaseQueryService _purchaseQuery;
    
    public event EventHandler? MovementChanged;

    public ObservableCollection<MovementViewModel> Movements{get;} = [];

    public IEnumerable<MovementViewModel> FilteredList(Filters filter)
    => Movements.Where(Selector(filter));
    
    public ObservableCollection<TransactionViewModel> Transactions {get;} = [];

    private void Update(IEnumerable<Guid>? args)
    {

        if(args is null)
        {
            Movements.Clear();
            UpdateTransactions(_movementQuery.GetMovements());
        }
        else
            foreach(var id in args)
            {
                var movements = _movementQuery.GetMovementsByTransactionId(id);
                UpdateTransactions(movements);
            }

        MovementChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateTransactions(IEnumerable<MovementDto> movements)
    {
        TransactionViewModel currentTransaction = null;
        foreach (var m in movements.OrderBy(x => x.TransactionId).OrderBy(x => x.Id))
        {
            var model = _mapper.ToModel(m);

            if (currentTransaction is not null && currentTransaction.TransactionId != m.TransactionId)
                Transactions.Add(currentTransaction);

            if (currentTransaction is null || currentTransaction.TransactionId != m.TransactionId)
            {
                string? description;
                DateTime date;
                string party;
                if (m.MovementType == MovementType.In)
                {
                    var t = _purchaseQuery.GetPurchaseById(m.TransactionId);
                    party = t.SupplierId.ToString();
                    date = t.Date;
                    description = t.Description;
                }
                else
                {
                    var t = _saleQuery.GetSaleById(m.TransactionId);
                    party = t.ClientKey.Key;
                    date = t.Date;
                    description = t.Description;
                }

                currentTransaction = new(date, m.TransactionId, description, party, m.MovementType, []);
            }
            
            currentTransaction.AddMovement(model);
            Movements.Add(model);   
        }
        if(currentTransaction is not null)
            Transactions.Add(currentTransaction);
    } 
}