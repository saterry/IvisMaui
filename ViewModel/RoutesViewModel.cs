using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IvisMaui.Data;
using IvisMaui.Models;
using IvisMaui.View;

namespace IvisMaui.ViewModel;

public partial class RoutesViewModel : BaseUIViewModel
//: ObservableObject, IRecipient<SelectBusMessage>
{
    public RoutesViewModel(ISqliteService sqliteservice) : base(sqliteservice)
    {
        base.LoadRoutes(Global.Bus);
        Busnumber = Global.BusNumber;
        Global.Form = "Route";
        if (!String.IsNullOrEmpty(Global.GpsStatus))
        {
            base.baseviewmodel.StopLocationUpdates();
        }

    }

    public void OnAppearing()
    {
        Global.isStopped = false;
        Global.Form = "Route";
        Global.RouteId = "";
        if (!String.IsNullOrEmpty(Global.GpsStatus))
        {
            base.baseviewmodel.StopLocationUpdates();
        }
    }

    [RelayCommand]
    async Task GoToBusStops(Route Route)
    {
        if (Route == null)
        return;

        Global.RouteId = Route.RouteId;
        Global.AmPm = Route.AmPm;
        Global.Route = Route;

        await Shell.Current.GoToAsync(nameof(
            BusStopPage), true, new Dictionary<string, object>
        {
            {"Bus", Global.Bus }
        });
    }
}