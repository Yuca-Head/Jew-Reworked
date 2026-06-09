using CommunityToolkit.Mvvm.ComponentModel;
using Jew.Avalonia.ViewModels.Products.Menus;
using Jew.Avalonia.ViewModels.Purchases.Menus;
using Jew.Avalonia.ViewModels.SideBar;

namespace Jew.Avalonia.ViewModels;

public partial class MainWindowViewModel(SidebarViewModel sideBar) : ViewModelBase
{
    [ObservableProperty]
    private SidebarViewModel _sideBar = sideBar;

    

    public string Greeting { get; } = "Welcome to Avalonia!";

    /*
    private void MetodoNoSé()
    =>  SideBar.OnOptionSelected = option =>
    {
        CurrentView = option switch
        {
            SideBarViewModel.MainOptions.Product => null,
            SideBarViewModel.MainOptions.Category => null,
            _ => CurrentView
        };
    };
    */
}
