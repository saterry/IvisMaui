using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IvisMaui.Data;
using IvisMaui.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Route = IvisMaui.Models.Route;

namespace IvisMaui.ViewModel;

public partial class BaseUIViewModel : ObservableObject, IRecipient<SelectBusMessage>
//: ObservableObject
{
    private readonly Task initTask;

    [ObservableProperty]
    public string busnumber;

    //[ObservableProperty]
    //public bool isStopped;

    [ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(IsNotBusy))]
    //Bus bus = Global.Bus;
    Bus bus;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    string title;

    public BaseViewModel baseviewmodel;

    partial void OnBusChanged(Bus value)
    {
        Console.WriteLine(value);
        //LoadStudents(value);
    }

    ISqliteService sqliteservice;
    public BaseUIViewModel(ISqliteService sqliteservice)
    {
        WeakReferenceMessenger.Default.Register<SelectBusMessage>(this);

        this.sqliteservice = sqliteservice;
        this.initTask = InitAsync();

        //Global.Form = "";
        //Global.RouteId = "";
        //Global.StopId = "";
        //baseviewmodel = BaseViewModel.Instance(sqliteservice);
        baseviewmodel = new BaseViewModel(sqliteservice);

        //if (Global.BusId == "0")
        //{
        //    var buses = await sqliteservice.BusList();

        //    App.Current.Resources["UnloadBusButtonVisible"] = false;
        //    //selectedBus.SelectedIndex = 0;
        //    if (buses.Count == 1)
        //    {
        //        //If there's only one bus then hide the select bus option.
        //        //Shell.//.SetTabBarIsVisible("SelectBus", false);
        //        App.Current.Resources["SelectBusTabVisible"] = false;
        //    }
        //    else
        //    {
        //        App.Current.Resources["SelectBusTabVisible"] = true;
        //    }

        //    if (buses.Count > 0)
        //    {
        //        Global.Bus = buses[0];
        //        Global.BusId = buses[0].BusId;
        //        Global.BusNumber = buses[0].Number;
        //    }
        //    else
        //    {
        //        //We've got a problem.
        //    }
        //}
    }

    private async Task InitAsync()
    {
        if (Global.BusId == "0")
        {
            var buses = await sqliteservice.BusListAsync();

            App.Current.Resources["UnloadBusButtonVisible"] = false;
            //selectedBus.SelectedIndex = 0;
            if (buses.Count == 1)
            {
                //If there's only one bus then hide the select bus option.
                //Shell.//.SetTabBarIsVisible("SelectBus", false);
                App.Current.Resources["SelectBusTabVisible"] = false;
            }
            else
            {
                App.Current.Resources["SelectBusTabVisible"] = true;
            }

            if (buses.Count > 0)
            {
                Global.Bus = buses[0];
                Global.BusId = buses[0].BusId;
                Global.BusNumber = buses[0].Number;
            }
            else
            {
                //We've got a problem.
            }
        }
    }

    #region messages
    public ObservableCollection<Busmessage> Messages { get; } = new();

    public async Task LoadMessages(Bus bus)
    {
        if (IsBusy)

            return;

        try
        {
            IsBusy = true;
            var messages = await sqliteservice.MessageList(bus.BusId);

            if (Messages.Count != 0)
                Messages.Clear();

            foreach (var message in messages)
                Messages.Add(message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get messages: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            //IsRefreshing = false;
        }
    }

    #endregion region

    #region routes

    public ObservableCollection<Route> Routes { get; } = new();

    [RelayCommand]
    public async Task LoadRoutes(Bus bus)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var routes = await sqliteservice.LoadRoutes(bus.BusId);
            //var routes = await routeService.GetRoutes();

            if (Routes.Count != 0)
                Routes.Clear();

            foreach (var route in routes)
                Routes.Add(route);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get routes: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            //IsRefreshing = false;
        }
    }

    #endregion

    #region students
    public ObservableCollection<Student> Students { get; } = new();

    [RelayCommand]
    public async Task LoadStudents(Bus bus)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var students = await sqliteservice.LoadStudents(bus.BusId);

            if (Students.Count != 0)
                Students.Clear();

            foreach (var student in students)
                Students.Add(student);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get students: {ex.Message}");
            Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            //IsRefreshing = false;
        }
    }
    #endregion

    public void Receive(SelectBusMessage message)
    {
        //Task.Run(async () => await FindAudioFiles(progress1));
        //Task.Run(() =>
        if (!MainThread.IsMainThread)
        {
            Console.WriteLine("Not main thread");
        }
        else
        {
            Console.WriteLine("Main thread");
        }

    MainThread.BeginInvokeOnMainThread(async () =>
        {
            //Add form specific functions
            //if (Global.Form == "Student")
            //{
            //await LoadMessages(Global.Bus);
            await LoadMessages(message.Value);
            await LoadRoutes(message.Value);
            await LoadStudents(message.Value);
            //Busnumber = Global.BusNumber;
            Busnumber = message.Value.Number;
            //}
        });
    }

    //public void StopGPSService()
    //{
    //    baseviewmodel.StopGPSService();
    //}
}
