using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using Jew.Avalonia.ViewModels;
using Jew.Avalonia.ViewModels.Movements.Menus;
using Jew.Avalonia.ViewModels.Movements.Outputs;
using Jew.Domain.InventoryMovements.Entities;

namespace Jew.Avalonia.Views.Movements;

public partial class MovementsMenuView : UserControl
{
    public MovementsMenuView()
    {
        InitializeComponent();
    }
    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        var source = e.Source as Control;

        if (source?.FindAncestorOfType<DataGridRow>() is null)
            return;

        if (DataContext is not MovementsMenuViewModel vm ||
            vm.SelectedTransaction is null)
            return;

        var mdw = new MovementDetailsWindow
        {
            DataContext = await vm.GetDetailedTransaction()
        };
        mdw.PartyType.Text = vm.SelectedTransaction.MovementType == MovementType.In ? "Proveedor" : "Cliente";

        mdw.Show();
    }
}