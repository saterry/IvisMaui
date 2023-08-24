using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IvisMaui.Data;
using IvisMaui.Models;
using IvisMaui.Services;
using IvisMaui.View;
//using RfId.Core.Enums;
//using RfId.Models.Events;
//using System.Text.RegularExpressions;

namespace IvisMaui.ViewModel;

public partial class BaseViewModel : ObservableObject
{

    private readonly LocationService _locationService;

    [ObservableProperty]
    private bool _locationUpdatesEnabled;

    public static BaseViewModel instance;

    private Rfid.RfId rfid;
    private string CardNumber;
    //private string StudentNumber;
    private string latitude;
    private string longitude;
    //public GpsDataEventArgs gpsposition;
    //public bool stopped = false;
    //private GPS.GPS gps;

    private static List<Busstop> GeoFences;

    ISqliteService sqliteservice;

    public static BaseViewModel Instance(ISqliteService sqliteservice)
    {
        if (instance == null)
        {
            instance = new BaseViewModel(sqliteservice);
        }
        return instance;
    }


    public BaseViewModel(ISqliteService sqliteservice)
    {
        this.sqliteservice = sqliteservice;
        //Updates = new();
        _locationService = new();

        //gps = GPS.GPS.Instance(sqliteservice);
        //rfid = Rfid.RfId.Instance();
        if (!String.IsNullOrEmpty(Global.RouteId))
        {
            //  xxx 1/16/2023
            if (string.IsNullOrEmpty(Global.GpsStatus))
            {
                StartLocationUpdates();
                MainThread.BeginInvokeOnMainThread(async () => { 
            //Task.Run(async () => { 
                await Form_Load(); });
                //Form_Load();
            }
        }
    }

    public void StartLocationUpdates()
    {
        _locationService.LocationChanged += LocationService_LocationChanged;
        _locationService.StatusChanged += LocationService_StatusChanged;
        _locationService.Initialize();
    }

    public void StopLocationUpdates()
    {
        _locationService.Stop();
        _locationService.LocationChanged -= LocationService_LocationChanged;
        _locationService.StatusChanged -= LocationService_StatusChanged;
    }

    private void LocationService_StatusChanged(object sender, string e)
    {
        //Updates.Add(e);
        string status = "";
        if (e == "LocationService->Initialize")
        {
            status = "Connected";
        }
        else if (e == "LocationService->Stop")
        {
            status = "";
        }

        if (!MainThread.IsMainThread)
        {
            MainThread.BeginInvokeOnMainThread((Action)delegate ()
            {

                //        if (InvokeRequired)
                //{
                //    BeginInvoke((Action)delegate ()
                {
                    //GpsStatuslabel.Text = status;
                    Global.GpsStatus = status;
                }
            });
        }
        else
        {
            //GpsStatuslabel.Text = status;
            Global.GpsStatus = status;
        }

    }

    private async void LocationService_LocationChanged(object sender, Models.LocationModel e)
    {
        if (Global.GpsStatus == "Connected")
        {
            try
            {
                if (GeoFences != null)  //No fences then no route.
                {

                    var speedF = e.Speed;//position.Speed;
                    //If slowing down then check if we're within a geofence.
                    if (speedF < 15.0)
                    {
                        if (!Global.isStopped)
                        {
                            Global.isStopped = true;
                            //todo if busStopDetailForm is not displayed do the following
                            //if (Global.Form != "BusStopDetail")
                            if (Global.Form == "Route")// || (Global.Form == "BusStop"))
                            {
                                //latitude = Latitude;
                                //longitude = Longitude;
                                await CheckFences(e.Latitude.ToString(), e.Longitude.ToString());
                            }
                            else if (Global.Form == "BusStopDetail")
                            {
                                if (!MainThread.IsMainThread)
                                {
                                    MainThread.BeginInvokeOnMainThread(() =>
                                    {
                                        displayBusStop();
                                    });
                                }
                                else
                                {
                                    displayBusStop();
                                }
                            }
                            else if (Global.Form == "BusStop")
                            {
                                if (!MainThread.IsMainThread)
                                {
                                    MainThread.BeginInvokeOnMainThread(() =>
                                    {
                                        displayBusStop();
                                    });
                                }
                                else
                                {
                                    displayBusStop();
                                }
                            }
                        }
                    }
                    else
                    {
                        Global.isStopped = false;
                        if (Global.Form == "BusStopDetail")
                        {
                            if (!MainThread.IsMainThread)
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    displayBusStop();
                                });
                            }
                            else
                            {
                                displayBusStop();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                longitude = "0";
                latitude = "0";
            }
        }
    }


    private async Task Form_Load()
    {
        await LoadFences();
    }


    //public async Task RfIdDataChanged(object sender, RfIdDataEventArgs e)
    //{
    //    if (rfid.status == RfIdStatus.Connected)
    //    {
    //        try
    //        {
    //            //if (Global.Form != "BusStopDetail")
    //            //{

    //            //TODO get Bus Stop for scan card.
    //            //Lookup stopid for student in current route.
    //            //StudentNumber = e.StudentNumber;
    //            CardNumber = e.CardNumber;
    //            CardNumber = Regex.Replace(CardNumber, @"[^\u0000-\u007F]+", string.Empty);
    //            CardNumber = CardNumber.Replace(Environment.NewLine, string.Empty);
    //            CardNumber = CardNumber.Replace("\u0002", String.Empty).Trim();
    //            CardNumber = CardNumber.Replace("\u0003", String.Empty).Trim();
    //            //CardNumber = CardNumber.Replace(Environment., string.Empty);
    //            //if (CardNumber == null)
    //            //{
    //            //    //Set message box background to red.
    //            //    var message = "Student not found.";
    //            //    MessageBox.Show(message, "Message");
    //            //}
    //            //else 
    //            //{ 

    //            if (Global.PrevCardNumber != CardNumber)
    //            {
    //                var studentstopdetail = await sqliteservice.GetStopIdForStudentCardInRoute(CardNumber, Global.RouteId, Global.AmPm);
    //                Global.PrevCardNumber = CardNumber;
    //                //}
    //                //if (studentstopdetail == null)
    //                //{
    //                //    //Set message box background to red.
    //                //    var message = "Student not found.";
    //                //    MessageBox.Show(message, "Message");
    //                //}
    //                //else
    //                //{
    //                var StudentNumber = "";
    //                var StopId = "";
    //                var Name = "";
    //                var Phone = "";
    //                var Grade = "";
    //                var DOB = "";
    //                var Status = 0;
                    
    //                if (studentstopdetail != null)
    //                {
    //                    StudentNumber = studentstopdetail.StudentNumber;
    //                    StopId = studentstopdetail.StopId.ToString();
    //                    Name = studentstopdetail.FullName.ToString();
    //                    Phone = studentstopdetail.Phone.ToString();
    //                    Grade = studentstopdetail.Grade.ToString();
    //                    DOB = studentstopdetail.DOB.ToString();
    //                    Status = studentstopdetail.Status;
    //                    Global.StopId = StopId;
    //                    Global.Intersection1 = studentstopdetail.Intersection1;
    //                    Global.Intersection2 = studentstopdetail.Intersection2;
    //                }

    //                if (!MainThread.IsMainThread)
    //                {
    //                    MainThread.BeginInvokeOnMainThread(() =>
    //                    {
    //                        //Todo Pass card, toggle status, display student
    //                        displayBusStopDetailUpdateStudent(StudentNumber, StopId, Global.RouteId, Global.AmPm, Name, Phone, Grade, DOB, Status);
    //                    });
    //                }
    //                else
    //                {
    //                    //Todo Pass card, toggle status, display student
    //                    //Task.Run(async () => await 
    //                    await displayBusStopDetailUpdateStudent(StudentNumber, StopId, Global.RouteId, Global.AmPm, Name, Phone, Grade, DOB, Status);
    //               //);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //        }
    //    }
    //}

    public async Task LoadFences()
    {
        GeoFences = await sqliteservice.LoadGeoFenceList();
    }

    //###################################################################
    //## Is latitude/longitude point inside a circle
    //###################################################################
    double GetDistance(double lat1, double lon1, double lat2, double lon2)
    {
        Location currentLocation = new Location(lat1, lon1);
        Location targetLocation = new Location(lat2, lon2);

        double distance = Location.CalculateDistance(currentLocation, targetLocation, DistanceUnits.Miles);

        return distance;
    }

    public async Task displayBusStopDetailUpdateStudent(string
    StudentNumber, string StopId, string RouteId, string AmPm, string Name, string Phone, string Grade, string DOB, int Status)
    //public void updateGUI(string StopId)
    {
        //TODO Set variable that GUI is open
        //Invoke an event to do the following.
        Global.Form = "BusStopDetail";
        var busStopDetailForm = new BusStopDetailsViewModel(sqliteservice);

        if (!string.IsNullOrEmpty(StudentNumber))
        {
            if (Status == 1)
            {
                Status = 0;
            }
            else
            {
                Status = 1;
            }
            await busStopDetailForm.UpdateStudentStatus(StudentNumber, StopId, RouteId, AmPm, Status);
        }
        //Display Bus Stop Details Page

        var Busstop = new Busstop();
        Busstop.StopId = StopId;
        Busstop.RouteId = Int32.Parse(RouteId);
        Busstop.AmPm = AmPm;
        Busstop.Intersection1 = Global.Intersection1;
        Busstop.Intersection2 = Global.Intersection2;

        var Studentstopdetail = new Studentstopdetail();
        if (!string.IsNullOrEmpty(StudentNumber)) 
        { 
            Studentstopdetail = await sqliteservice.LoadStudentStopDetail(StudentNumber, StopId, AmPm);
        }
        try
        {

            await Shell.Current.GoToAsync(nameof(BusStopDetailsPage), true, new Dictionary<string, object>
            {
                {"Busstop", Busstop },
                {"Studentstopdetail", Studentstopdetail }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
        }
    }

    public async Task displayBusStopDetail(string StopId)
    {
        //TODO Set variable that GUI is open
        //Invoke an event to do the following.
        Global.StopId = StopId;
        Global.Form = "BusStopDetail";
        var busStopDetailForm = new BusStopDetailsViewModel(sqliteservice);
        var Busstop = await sqliteservice.GetBusStopByStopIdInRoute(StopId, Global.RouteId, Global.AmPm);

        await Shell.Current.GoToAsync(nameof(BusStopDetailsPage), true, new Dictionary<string, object>
            {
                {"Busstop", Busstop }
            });

    }

    public async void displayBusStop()
    {
        //TODO Set variable that GUI is open
        //Invoke an event to do the following.
        Global.Form = "BusStop";
        var busStopForm = new BusStopsViewModel(sqliteservice);
        await busStopForm.LoadBusStops();
    }

    public async Task CheckFences(string latitude, string longitute)
    {
        double originlatitude = float.Parse(latitude);
        double originlongitute = float.Parse(longitute);
        double targetlatitude;
        double targetlongitude;
        double distance;
        //loop through fences and call GetDistance
        foreach (var fence in GeoFences)
        {
            targetlatitude = float.Parse(fence.Latitude);
            targetlongitude = float.Parse(fence.Longitude);
            distance = GetDistance(originlatitude, originlongitute, targetlatitude, targetlongitude);
            //Just in case event doesn't exist
            //TODO Double check commenting this out.
            //If we're within 480 meters of the location then display the BusStopDetailScreen!
            //if (distance < 740)  //640 meters ~ 700 yards
            if (distance < 0.5)  //640 meters ~ 700 yards
            {
                //if (fence.Intersection1 == "")
                //{
                //    UnloadBtn.Visible = true;
                //}
                var busstop = await sqliteservice.GetBusStopByStopIdInRoute(fence.StopId, Global.RouteId, Global.AmPm);

                if (busstop != null)
                {
                    Global.Intersection1 = busstop.Intersection1;
                }

                if (!MainThread.IsMainThread)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await displayBusStopDetail(fence.StopId);
                    });
                }
                else
                {
                    await displayBusStopDetail(fence.StopId);
                }
            }
        }
    }

    //void RfIdStatusChanged(object sender, RfIdStatus e)
    //{
    //    string status = "";
    //    if (e == RfIdStatus.Connected)
    //    {
    //        status = "Connected";
    //    }
    //    else if (e == RfIdStatus.Connecting)
    //    {
    //        status = "Connecting";
    //    }
    //    else if (e == RfIdStatus.Disabled)
    //    {
    //        status = "Disabled";
    //    }

    //    //Task.Run(async () => Global.RfIdStatus = status);
    //    if (!MainThread.IsMainThread)
    //    {
    //        MainThread.BeginInvokeOnMainThread(() =>
    //        {
    //            Global.RfIdStatus = status;
    //        });
    //    }
    //    else
    //    {
    //        Global.RfIdStatus = status;
    //    }
    //}

    //async Task startRfId()
    ////void startRfId()
    //{
    //    //rfid.RegisterDataChangedEvent(RfIdDataChanged);
    //    rfid.RegisterStatusChangedEvent(RfIdStatusChanged);

    //    Global.RfIdTokenSource = new CancellationTokenSource();
    //    CancellationToken token = Global.RfIdTokenSource.Token;

    //    await Task.Run(() => rfid.StartRfId(token), token);
    //    //rfid.StartRfId(token);//, token;
    //}

    //void stopRfId()
    //{
    //    rfid.ResetDataChangedEvent();
    //    if (Global.RfIdTokenSource != null)
    //    {
    //        Global.RfIdTokenSource.Cancel();
    //    }
    //}

}