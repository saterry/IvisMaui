//using Android.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using IvisMaui.Data;
using IvisMaui.Models;
using IvisMaui.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IvisMaui.ViewModel;

[QueryProperty(nameof(Busstop), "Busstop")]
[QueryProperty(nameof(Studentstopdetail), "Studentstopdetail")]
public partial class BusStopDetailsViewModel : ObservableObject, IRecipient<SelectBusMessage>
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    public string busnumber;

    public ObservableCollection<Studentstopdetail> StudentStopDetails { get; } = new();

    [ObservableProperty]
    public Studentstopdetail studentstopdetail;

    //public Studentstopdetail SelectedItem;

    public string Intersections = "";

    ISqliteService sqliteservice;

    public BusStopDetailsViewModel(ISqliteService sqliteservice) //: base(sqliteservice)
    {
        WeakReferenceMessenger.Default.Register<SelectBusMessage>(this);

        //Global.Form = "Route";
        //Global.Form = "BusStopDetail";

        App.Current.Resources["UnloadBusButtonVisible"] = false;

        this.sqliteservice = sqliteservice;

        LoadStudentStopStatus(Global.Intersection1, Global.AmPm, Global.RouteId, Global.StopId);
    }

    [ObservableProperty]
    Busstop busstop;

    [ObservableProperty]
    bool isRefreshing;

    public void OnAppearing()
    {
        Global.Form = "BusStopDetail";
    }

    public async Task UnloadBus()
    {
        sqliteservice.UnloadBus(Global.BusNumber);

        await Shell.Current.GoToAsync(nameof(BusStopPage), true, new Dictionary<string, object>
        {
            {"Bus", Global.Bus }
        });
    }

    public async Task LoadStudentStopStatus(string Intersection1, string AmPm, string RouteId, string StopId)
    {
        List<Studentstopdetail> studentstopdetails = new List<Studentstopdetail>();


        if (Intersection1.ToLower() == "arrive" && AmPm == "0")
        {
            studentstopdetails = await sqliteservice.LoadStudentOnBusDetails();
            App.Current.Resources["UnloadBusButtonVisible"] = true;
        }
        else if (Intersection1.ToLower() == "arrive" && AmPm == "1")
        {
            studentstopdetails = await sqliteservice.LoadStudentBoardingBusDetails(RouteId);
        }
        else
        {
            studentstopdetails = await sqliteservice.LoadStudentStopDetails(StopId, AmPm);
        }

        if (StudentStopDetails.Count != 0)
            StudentStopDetails.Clear();

        foreach (var student in studentstopdetails)
            StudentStopDetails.Add(student);

        if (StudentStopDetails.Count > 0)
        {
            Intersections = StudentStopDetails[0].Intersection1 + " & " + StudentStopDetails[0].Intersection2;
        }
    }
    public async Task UpdateStudentStatus(string StudentNumber, string StopId, string RouteId, string AmPm, int Status)
    {
        //sqliteservice.SaveStatus(ssd.BusNumber, RouteId, StopId, StudentNumber, Status, AmPm);

        //LoadStudentStopStatus(ssd.Intersection1, AmPm, RouteId, StopId);

        sqliteservice.SaveStatus(Global.BusNumber, RouteId, StopId, StudentNumber, Status, AmPm);

        LoadStudentStopStatus(Global.Intersection1, AmPm, RouteId, StopId);

    }


    //public void UpdateStatus(string StudentNumber, string StopId, string RouteId, string AmPm, int Status)
    public void UpdateStatus(Studentstopdetail ssd)
    {
        sqliteservice.SaveStatus(ssd.BusNumber, ssd.RouteId, ssd.StopId, ssd.StudentNumber, ssd.Status, ssd.AmPm);
        LoadStudentStopStatus(ssd.Intersection1, ssd.AmPm, ssd.RouteId, ssd.StopId);
    }

    [RelayCommand]
    public void ToggleItem(Studentstopdetail studentstopdetail)
    //public async Task ToggleItemControl(Studentstopdetail studentstopdetail)
    {
        //Console.WriteLine($"toggle {o}");
        //string StudentNumber = studentstopdetail.StudentNumber;//row.Cells["StudentNumber"].Value.ToString();
        //UpdateStatus(StudentNumber, o.StopId, o.Global.RouteId, o.Global.AmPm, o.Status);
        if (studentstopdetail.Status == 0)
            studentstopdetail.Status = 1;
        else
            studentstopdetail.Status = 0;

        UpdateStatus(studentstopdetail);
    }


    partial void OnBusstopChanged(Busstop value)
    {
        GetBusStopStudents(value);
    }


    //partial void OnStudentstopdetailChanged(Studentstopdetail value)
    //{
    //    //GetBusStopStudents(value);
    //    studentstopdetail = value;

    //}

    [RelayCommand]
    async Task SetStudentDetails(Studentstopdetail Studentstopdetail)
    {
        //        studentstopdetail = Studentstopdetail;
        if (studentstopdetail != null) {
            await Shell.Current.GoToAsync("..");
        }
        Global.Form = "Busstopdetailsstudent";
        await Shell.Current.GoToAsync(nameof(
            BusStopDetailsPage), true, new Dictionary<string, object>
        {
            {"Busstop", Busstop },
            {"Studentstopdetail", Studentstopdetail }
        });


    }


    void GetBusStopStudents(Busstop busstop)
    {
        if (IsBusy)
            return;
        List<Studentstopdetail> studentstopdetails = new List<Studentstopdetail>();

        try
        {
            IsBusy = true;
            this.busstop = busstop;

            LoadStudentStopStatus(busstop.Intersection1, busstop.AmPm, busstop.RouteId.ToString(), busstop.StopId);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get busstop: {ex.Message}");
            Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    public void Receive(SelectBusMessage message)
    {
        //Task.Run(() =>
        MainThread.BeginInvokeOnMainThread(async () =>
        {

            StudentStopDetails.Clear();

            //LoadStudents(message.Value);
            //Busnumber = Global.BusNumber;
            //Routetitle = "Bus: " + Global.BusNumber + " Routes";
        });
    }

}