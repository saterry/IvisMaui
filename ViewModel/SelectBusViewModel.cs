using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IvisMaui.Data;
using IvisMaui.Models;
using IvisMaui.View;
using System.Collections.ObjectModel;
using System.Diagnostics;
//using static System.Net.Mime.MediaTypeNames;

namespace IvisMaui.ViewModel;

public partial class SelectBusViewModel : ObservableObject, IRecipient<SelectBusMessage>
{

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    public string busnumber;

    [ObservableProperty]
    public Bus bus;

    [ObservableProperty]
    bool isRefreshing;

    //[ObservableProperty]
    //public bool isVisible;

    public ObservableCollection<Bus> Buses { get; } = new();

    ISqliteService sqliteservice;

    public SelectBusViewModel
        (ISqliteService sqliteservice)// : base(sqliteservice)
    {
        WeakReferenceMessenger.Default.Register<SelectBusMessage>(this);
        this.sqliteservice = sqliteservice;
        GetBuses();
        Busnumber = Global.BusNumber;

        //IsVisible = true;
    }

    [RelayCommand]
    async Task GoToMessages(Bus bus)
    {
        if (bus == null)
        return;
        Global.Bus = bus;
        Global.BusId = bus.BusId;
        Global.BusNumber = bus.Number;
        Bus = bus;

        WeakReferenceMessenger.Default.Send(new SelectBusMessage(bus));
        //Hide
        //isVisible = false;

        //await Shell.Current.GoToAsync(nameof(SelectBusDetailsPage), false, new Dictionary<string, object>
        //{
        //    {"Bus", bus }
        //});
    }

    [RelayCommand]
    async Task GetBuses()
    {
        if (IsBusy)
            return;

        try
        {

            IsBusy = true;
            var buses = await sqliteservice.BusListAsync();
            //var routes = await routeService.GetRoutes();

            if(Buses.Count != 0)
                Buses.Clear();

            foreach(var bus in buses)
                Buses.Add(bus);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get buses: {ex.Message}");
            Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
    public void Receive(SelectBusMessage message)
    {
        //Task.Run(() =>
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Busnumber = message.Value.Number;//Global.BusNumber;
        });
    }
}
