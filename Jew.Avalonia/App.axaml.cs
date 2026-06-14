using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Jew.Avalonia.ViewModels;
using Jew.Avalonia.Views;
using System;
using Jew.Applications.InventoryMovements.Commands;
using Jew.Applications.ProductInventory.Queries;
using Jew.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Jew.Avalonia.Views.Products;
using Jew.Avalonia.ViewModels.Products.Menus;
using Jew.Avalonia.ViewModels.SideBar;
using Jew.Applications.ProductInventory.Commands;
using Jew.Applications.InventoryMovements.Queries;
using Jew.Avalonia.ViewModels.Products.Services;
using Jew.Avalonia.Shared.Contexts;
using System.Dynamic;
using Jew.Avalonia.ViewModels.Purchases.Menus;
using Jew.Applications.Purchases.Queries;
using Jew.Applications.Purchases.Commands;
using Jew.Avalonia.ViewModels.Inventory.Menus;
using Jew.Avalonia.ViewModels.Sales.Menus;
using Jew.Applications.Sales.Queries;
using Jew.Applications.Sales.Commands;
using Jew.Domain.InventoryMovements.Entities;

namespace Jew.Avalonia;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        #if DEBUG
            this.AttachDevTools();
        #endif

        var services = new ServiceCollection();

        ConfigureServices(services);

        Services = services.BuildServiceProvider();

        var context = Services.GetRequiredService<IUnitOfWork>();
        
        context.Load();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            var mainWindow = Services.GetRequiredService<MainWindow>();
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        // UoW
        services.AddSingleton<IUnitOfWork>(_= new JsonUnitOfWork(Infrastructure.Enums.Environment.Test));

        // Services de aplicación
        services.AddSingleton<StockStateQueryService>();
        services.AddSingleton<CategoryQueryService>();
        services.AddSingleton<ProductQueryService>();
        services.AddSingleton<MovementCommands>();
        services.AddSingleton<InventoryQueryService>();
        services.AddSingleton<InventoryCommands>();
        services.AddSingleton<SupplierQueryService>();
        services.AddSingleton<PurchaseCommands>();
        services.AddSingleton<ClientQueryService>();
        services.AddSingleton<SaleCommands>();
        services.AddSingleton<ProductStockState>();

        //UI Services
        services.AddSingleton<ProductState>();
        services.AddSingleton<InventoryState>();

        // ViewModels
        services.AddTransient<MainWindow>();
        services.AddSingleton<PurchaseSupplierCardVM>();
        services.AddSingleton<PurchasesMenuViewModel>();
        services.AddSingleton<ProductsMenuViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<SearchProductViewModel>();
        services.AddSingleton<SidebarViewModel>();
        services.AddSingleton<InventoryMenuViewModel>();
        services.AddSingleton<SalesMenuViewModel>();
    }   
}