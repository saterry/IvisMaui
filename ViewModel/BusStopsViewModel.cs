using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IvisMaui.Data;
using IvisMaui.Models;
using IvisMaui.View;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IvisMaui.ViewModel;

[QueryProperty(nameof(Bus), "Bus")]
public partial class BusStopsViewModel : BaseUIViewModel
{
    public ObservableCollection<Busstop> BusStops { get; } = new();

    ISqliteService sqliteservice;

    [ObservableProperty]
    Route route;

    public BusStopsViewModel(ISqliteService sqliteservice) : base(sqliteservice)
    {
        //Title = "Bus Stop";
        this.sqliteservice = sqliteservice;
        LoadBusStops();
    }

    public void OnAppearing()
    {
        Global.Form = "BusStop";
    }

    [RelayCommand]
    async Task GoToBusStopDetails(Busstop Busstop)
    {
        if (Busstop == null)
        return;
        Global.StopId = Busstop.StopId;
        Global.Intersection1 = Busstop.Intersection1;
        Global.Intersection2 = Busstop.Intersection2;
        Global.Busstop = Busstop;

        await Shell.Current.GoToAsync(nameof(BusStopDetailsPage), true, new Dictionary<string, object>
        {
            {"Busstop", Busstop }
        });

    }

    public async Task LoadBusStops()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var busstops = await sqliteservice.LoadBusStops(Global.RouteId, Global.BusId.ToString(), Global.AmPm);

            if(BusStops.Count != 0)
                BusStops.Clear();

            foreach(var busstop in busstops)
                BusStops.Add(busstop);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get busstops: {ex.Message}");
            Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}