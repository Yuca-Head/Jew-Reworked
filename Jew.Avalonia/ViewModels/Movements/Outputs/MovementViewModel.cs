using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Domain.InventoryMovements.Entities;

namespace Jew.Avalonia.ViewModels.Movements.Outputs;

public record MovementViewModel(string ProductCode, int Id, int Quantity, decimal Cost,
MovementType MovementType)
{
    public decimal Total {get;} = Quantity * Cost;
}