using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Jew.Avalonia.Views.Products;

public partial class ProductsMenuView : UserControl
{
    public ProductsMenuView()
    {
        InitializeComponent();
        Select(btnCreateProduct, btnModifyProduct);
    }

    private static void Select(Button selected, Button unselected)
    {
        selected.Background = new SolidColorBrush(Color.Parse("#31C1ABED"));
        selected.Foreground = new SolidColorBrush(Colors.Black);

        unselected.Background = new SolidColorBrush(Colors.White);
        unselected.Foreground = new SolidColorBrush(Color.Parse("#868686"));
    }

    private void CreateProduct_Click(object? sender, RoutedEventArgs e)
    {
        Select(btnCreateProduct, btnModifyProduct);
    }

    private void ModifyProduct_Click(object? sender, RoutedEventArgs e)
    {
        Select(btnModifyProduct, btnCreateProduct);
    }

}