using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jew.Avalonia.ViewModels.Products.Outputs;

namespace Jew.Avalonia.ViewModels.CommonViewModels;

public partial class ProductSearchBarViewModel(IEnumerable<ProductViewModelBase> WholeList, 
ObservableCollection<ProductViewModelBase> DisplayedList) : ViewModelBase
{
    //Busqueda
    [ObservableProperty]
    private string _searchCode = "";
    public ObservableCollection<ProductViewModelBase> Suggestions {get; private set;} = [];

    partial void OnSearchCodeChanged(string value)
    {
        Suggestions.Clear();

        if(string.IsNullOrWhiteSpace(value))
            return;

        foreach(var product in WholeList
            .Where(x => x.Code.StartsWith(value, StringComparison.OrdinalIgnoreCase))
            .Take(10)
            )
        {
            Suggestions.Add(product);
        }
    }


    [RelayCommand]
    private void SelectProduct(ProductViewModelBase product)
    {
        AddItem(product);

        SearchCode = "";
        Suggestions.Clear();
    }



    private void AddItem(ProductViewModelBase product)
    => DisplayedList.Add(product);
    
}