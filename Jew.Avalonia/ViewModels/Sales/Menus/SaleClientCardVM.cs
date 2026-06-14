using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Sales.Queries;
using Jew.Avalonia.ViewModels.Clients.Outputs;
using Jew.Avalonia.ViewModels.Products.Outputs;

namespace Jew.Avalonia.ViewModels.Sales.Menus;

public partial class SaleClientCardVM : ViewModelBase
{
    private readonly ClientQueryService _clientQuery;
    public ObservableCollection<ClientViewModel> Clients {get; set;} = [];

    [ObservableProperty]
    private ClientViewModel _clientSelected = defaultClient;
    private static readonly ClientViewModel defaultClient = new(new(5, "-----"), "Escoger Cliente");

    [ObservableProperty]
    private DateTimeOffset? _orderDate = DateTimeOffset.Now;

    [ObservableProperty]
    private Guid _transactionId = Guid.NewGuid();


    public SaleClientCardVM(ClientQueryService clientQuery)
    {   
        _clientQuery = clientQuery;
        Clients = [defaultClient ,.._clientQuery.GetClients().Select(x => new ClientViewModel(x.Code, x.Name))];
    }
    public void Clear()
    {
        ClientSelected = defaultClient;
        TransactionId = Guid.NewGuid();
    }


}