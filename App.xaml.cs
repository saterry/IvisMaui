using CommunityToolkit.Mvvm.Messaging;
using IvisMaui.Data;
using IvisMaui.Models;
using Microsoft.Maui.Storage;
using System.Reflection;
using System.Xml.Linq;

namespace IvisMaui;

public partial class App : Application
{
    static ISqliteService sqliteservice;

    public App(ISqliteService Sqliteservice)
    {
        sqliteservice = Sqliteservice;
        InitializeComponent();

        //Task.Run(async () => { await InitAsync(); });
        Init();

        MainPage = new AppShell();
    }

    private async Task InitAsync()
    {
        if (Global.Init == "")
        {
            Global.Init = "1";
        }

        if (Global.BusId == "0")
        {
            var buses = await sqliteservice.BusListAsync();

            if (buses.Count == 1)
            {
                //If there's only one bus then hide the select bus option.
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
                WeakReferenceMessenger.Default.Send(new SelectBusMessage(Global.Bus));
                //Busnumber = Global.BusNumber;
                //Pagetitle =  "Bus: " + Global.BusNumber + " Messages";
            }
            else
            {
                //We've got a problem.
            }
        }
    }

    private void Init()
    {
        if (Global.Init == "")
        {
            Global.Init = "1";
        }

        if (Global.BusId == "0")
        {
            var buses = sqliteservice.BusList();

            if (buses.Count == 1)
            {
                //If there's only one bus then hide the select bus option.
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
                WeakReferenceMessenger.Default.Send(new SelectBusMessage(Global.Bus));
                //Busnumber = Global.BusNumber;
                //Pagetitle =  "Bus: " + Global.BusNumber + " Messages";
            }
            else
            {
                //We've got a problem.
            }
        }
    }
}
