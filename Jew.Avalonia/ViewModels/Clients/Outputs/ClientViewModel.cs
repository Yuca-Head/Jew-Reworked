using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Applications.Sales.DTOs.Clients;
using Jew.Domain.Shared.Keys;

namespace Jew.Avalonia.ViewModels.Clients.Outputs;

public partial class ClientViewModel(CodeKey codekey, string name) : ViewModelBase
{
    [ObservableProperty]
    private CodeKey _codeKey = codekey;
    [ObservableProperty]
    private string _name = name;

    public static ClientViewModel From(ClientDto clientDto)
    => new(clientDto.Code, clientDto.Name);
}