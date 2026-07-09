using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jew.Applications.Purchases.DTOs.Suppliers;
using Jew.Applications.Purchases.Queries;
using Jew.Applications.Sales.DTOs.Clients;
using Jew.Applications.Sales.Queries;
using Jew.Avalonia.Shared.Contexts;
using Jew.Avalonia.ViewModels.Movements.Mappers;
using Jew.Avalonia.ViewModels.Movements.Outputs;
using Jew.Domain.InventoryMovements.Entities;
using Jew.Domain.Sales.Entities;
using Jew.Domain.Shared.Keys;

namespace Jew.Avalonia.ViewModels.Movements.Menus;

public partial class MovementsMenuViewModel : ViewModelBase
{

    [ObservableProperty]
    private MovementsState _movementState;
    [ObservableProperty]
    private TransactionViewModel _selectedTransaction;
    [ObservableProperty]
    private DateTimeOffset? _dateTil;
    [ObservableProperty]
    private DateTimeOffset? _dateSince;



    private ObservableCollection<DetailedTransactionViewModel> DetailedTransactions { get; } = [];

    [ObservableProperty]
    private ObservableCollection<TransactionViewModel> _displayedTransactions;

    private readonly ClientQueryService _clients;
    private readonly SupplierQueryService _suppliers;
    private readonly ProductState _products;

    private readonly Dictionary<string, ClientDto> _registeredClients = [];
    private readonly Dictionary<string, SupplierDto> _registeredSuppliers = [];




#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public MovementsMenuViewModel
    (MovementsState movementsState, ProductState products, ClientQueryService clients, SupplierQueryService suppliers)
    {
        MovementState = movementsState;

        MovementState.MovementChanged += (_, _) => {AddFilters(); UpdateGeneralInfo();};
        UpdateGeneralInfo();
        DisplayedTransactions = new(MovementState.Transactions);
        _products = products;
        _clients = clients;
        _suppliers = suppliers;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


    [ObservableProperty]
    private string? _selectedType;
    [ObservableProperty]
    private int _inAmount;
    [ObservableProperty]
    private decimal _inTotal;
    [ObservableProperty]
    private int _outAmount;
    [ObservableProperty]
    private decimal _outTotal;

    public string[] MovementTypes {get;} = ["Entrada", "Salida"];

    [RelayCommand]
    private void AddFilters()
    {
        IEnumerable<TransactionViewModel> query = MovementState.Transactions;
        query = FilterDate(query);
        query = FilterMovementType(query);

        DisplayedTransactions = [.. query];
    }
    
    [RelayCommand]
    private void RemoveFilters()
    {
        if (DisplayedTransactions.Count != MovementState.Transactions.Count)
            DisplayedTransactions = MovementState.Transactions;
        DateTil = null;
        DateSince = null;
        SelectedType = null;
    }
    private IEnumerable<TransactionViewModel> FilterMovementType(IEnumerable<TransactionViewModel> query)
    {
        if(SelectedType is null)
            return query;

        return query.Where(x => x.MovementType == GetMovementByString(SelectedType));        
    }

    private MovementType GetMovementByString(string type)
    => type == MovementTypes[0] ? MovementType.In : MovementType.Out;
    private IEnumerable<TransactionViewModel> FilterDate(IEnumerable<TransactionViewModel> query)
    {
        if(DateTil is null && DateSince is null)
            return query;
        DateTime dateTil = DateTil.HasValue ? DateTil.Value.Date : DateTime.Today.Date;
        DateTime dateSince = DateSince.HasValue ? DateSince.Value.Date : DateTime.MinValue.Date;

        return query.Where(x => x.Date.Date <= dateTil.Date && x.Date.Date >= dateSince.Date);
    }

    public async Task<DetailedTransactionViewModel> GetDetailedTransaction()
    {
        var id = SelectedTransaction.TransactionId;
        var existing = DetailedTransactions.FirstOrDefault(x => x.TransactionId == id);

        if (existing is not null)
            return existing;

        DetailedTransactionViewModel result = new(MovementState.Transactions.First(x => x.TransactionId == id));    

        if (result.MovementType == MovementType.In)
            if (_registeredSuppliers.TryGetValue(result.Party!, out SupplierDto? value))
                result.PartyName = value.Name;
            else
            {
                var supp = await _suppliers.GetSupplier(new(5, result.Party!));
                _registeredSuppliers[supp.CodeKey.Key] = supp;
                result.PartyName = supp.Name;
            }
        else
            if (_registeredClients.TryGetValue(result.Party!, out ClientDto? value))
                result.PartyName = value.Name;
            else
            {
                var client = await _clients.GetClient(new(5, result.Party!));
                _registeredClients[client.Code.Key] = client;
                result.PartyName = client.Name;
            }

        foreach (var m in result.Movements)
            result.DetailedMovements.Add(new(_products.GetProductById(m.ProductCode)
            ?? throw new InvalidOperationException($"No se encontró el producto {m.ProductCode} para el movimiento."), m));

        DetailedTransactions.Add(result);
        return result;
    }

    private void UpdateGeneralInfo()
    {
        (int Amount,decimal Total) ins = (0,0), outs = (0,0);

        foreach(var m in MovementState.Transactions)
        {
            if(m.MovementType == MovementType.In)
            {
                ins.Amount += m.Amount; 
                ins.Total += m.Total;
            }
            else
            {
                outs.Amount += m.Amount;
                outs.Total += m.Total;
            }

        }

        OutAmount = outs.Amount;
        OutTotal = outs.Total;
        InAmount = ins.Amount;
        InTotal = ins.Total;
    }

}