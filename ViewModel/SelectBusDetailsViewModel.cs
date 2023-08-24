using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IvisMaui.Data;
using IvisMaui.Models;
using System.Diagnostics;

namespace IvisMaui.ViewModel;

[QueryProperty(nameof(Bus), "Bus")]
public partial class SelectBusDetailsViewModel : ObservableObject
{
    //SqliteService sqliteservice;
    public SelectBusDetailsViewModel() 
    {
        //this.sqliteservice = sqliteservice;
    }

    [ObservableProperty]
    Bus bus;

    [ObservableProperty]
    bool isRefreshing;

}
