using IvisMaui.Data;
using IvisMaui.Models;
using IvisMaui.Services;
using GPS.Models.Events;
using IvisMaui.GPS.Enums;
using IvisMaui.GPS.Model;

namespace IvisMaui.GPS;

public class GPS
{
    //singleton
    public static GPS instance;

    //private string latitude;
    //private string longitude;

    #region Constructors
    //public GPS(Action<string> write)
    //{
    //    this.write = write;
    //}

    public GPS()
    {
    }

    //SqliteService sqliteservice;
    //static CancellationTokenSource _cts;

    public static GPS Instance(ISqliteService sqliteservice)
    {
        
        
        if (instance == null)
        {
            instance = new GPS();
        }
        return instance;
    }

    #endregion

    //private readonly Action<string> write;
    //private CancellationToken token;
    //public static GetLocationService _gpsService;
    public GpsDataEventArgs GpsPosition = new GpsDataEventArgs();

    //    public RfIdStatus status = RfIdStatus.Disabled;
    //public GpsStatus status = GpsStatus.Disabled;
    public static int counter = 0;
    //public event EventHandler<GpsDataEventArgs> GpsCallbackEvent;
    //public List<EventHandler<GpsDataEventArgs>> GpsCallbackEventdelegates = new List<EventHandler<GpsDataEventArgs>>();

    public event EventHandler<GpsStatus> GpsStatusEvent;
    public List<EventHandler<GpsStatus>> GpsStatusEventdelegates = new List<EventHandler<GpsStatus>>();

    //public void RegisterDataChangedEvent(Action<object, GPSDataEventArgs> action)
    //{
    //    GPSCallbackEvent += new EventHandler<GPSDataEventArgs>(action);
    //    GPSCallbackEventdelegates.Add(new EventHandler<GPSDataEventArgs>(action));
    //}

    //public void RegisterPositionChangedEvent(Action<object, GpsDataEventArgs> action)
    //{
    //    GpsCallbackEvent += new EventHandler<GpsDataEventArgs>(action);
    //    GpsCallbackEventdelegates.Add(new EventHandler<GpsDataEventArgs>(action));
    //}

    public void RegisterStatusChangedEvent(Action<object, GpsStatus> action)
    {
        GpsStatusEvent += new EventHandler<GpsStatus>(action);
        GpsStatusEventdelegates.Add(new EventHandler<GpsStatus>(action));
    }

    //public void ResetDataChangedEvent()
    //{
    //    foreach (EventHandler<GpsDataEventArgs> eh in GpsCallbackEventdelegates)
    //    {
    //        GpsCallbackEvent -= eh;
    //    }
    //    GpsCallbackEventdelegates.Clear();
    //}

    //public void StartGPS(CancellationToken token)
    //{
    //    this.token = token;
    //    _gpsService = new GetLocationService();
    //    _gpsService.RegisterStatusEvent(Action);
    //    _gpsService.RegisterDataEvent(GpsServiceOnLocationChanged);

    //    try
    //    {
    //        _gpsService.Connect();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //    }
    //}

    //private void Action(object o, GpsStatus gpsStatus)
    //{
    //    status = gpsStatus;
    //    GpsStatusEvent?.Invoke(this, status);
    //    Console.WriteLine(gpsStatus);
    //    if (write != null)
    //    {
    //        write(gpsStatus.ToString());
    //    }
    //}

    //private void GPSServiceOnDataChanged(object sender, GpsDataEventArgs e)
    //{
    //    //maybe check status?

    //    counter = counter + 1;
    //    if (write != null)
    //    {
    //        write($"[{counter}]" + e.ToString());
    //    }
    //    else
    //    {
    //        GpsCallbackEvent?.Invoke(this, e);
    //    }
    //    if (token.IsCancellationRequested)
    //    {
    //        if (write != null)
    //        {
    //            write("Cancel logging.");
    //        }
    //        _gpsService.Disconnect();
    //    }
    //}

    //private void GpsServiceOnLocationChanged(object sender, GpsDataEventArgs e)
    //{
    //    //maybe check status?

    //    counter = counter + 1;
    //    //Console.WriteLine($"[{counter}]");
    //    //latitude = e.Latitude;
    //    //longitude = e.Longitude;
    //    //speed = e.Speed;
    //    //write(e.ToString());
    //    if (write != null)
    //    {
    //        write($"[{counter}]" + e.ToString());
    //    }
    //    else
    //    {
    //        GpsPosition.Latitude = e.Latitude;
    //        GpsPosition.Longitude = e.Longitude;
    //        GpsPosition.Speed = e.Speed;

    //        GpsCallbackEvent?.Invoke(this, e);
    //        //writePosition(gpsPosition);
    //    }
    //    if (token.IsCancellationRequested)
    //    {
    //        if (write != null)
    //        {
    //            write("Cancel logging.");
    //        }
    //        _gpsService.Disconnect();
    //    }
    //}

    //public GPS(SqliteService sqliteservice)
    //{
    //    this.sqliteservice = sqliteservice;

    //    gps = GPS.RfId.Instance();
    //    if (!String.IsNullOrEmpty(Global.RouteId))
    //    {
    //        MainThread.BeginInvokeOnMainThread(async () => { 
    //        //Task.Run(async () => { 
    //            await Form_Load(); });
    //        //Form_Load();
    //    }
    //}


    //public void GPSStatusChanged(object sender, GpsStatus e)
    //{
    //    string status = "";
    //    if (e == GpsStatus.Connected)
    //    {
    //        status = "Connected";
    //    }
    //    else if (e == GpsStatus.Connecting)
    //    {
    //        status = "Connecting";
    //    }
    //    else if (e == GpsStatus.Disabled)
    //    {
    //        status = "Disabled";
    //    }

    //    if (InvokeRequired)
    //    {
    //        BeginInvoke((Action)delegate ()
    //        {
    //            //GpsStatuslabel.Text = status;
    //            Global.GpsStatus = status;
    //        });
    //    }
    //    else
    //    {
    //        //GpsStatuslabel.Text = status;
    //        Global.GpsStatus = status;
    //    }
    //}

    ////public void GPSPositionChanged(object sender, GPSPositionChangedEventArgs e) // Find GeoLocation of Device  
    ////public async Task GPSPositionChanged(double Speed, string Latitude, string Longitude)
    //public async Task GPSPositionChanged(object sender, GPSDataEventArgs e)
    //{
    //    //if (gps.status == GpsStatus.Connected)
    //    //{
    //    try
    //        {
    //            if (GeoFences != null)  //No fences then no route.
    //            {

    //                var speedF = Speed;
    //                //If slowing down then check if we're within a geofence.
    //                if (speedF < 15.0)
    //                {
    //                    //latitude = gps.gpsPosition.Position.Latitude.ToString();
    //                    //longitude = gps.gpsPosition.Position.Longitude.ToString();

    //                    //todo if busStopDetailForm is not displayed do the following
    //                    //if (Global.Form != "BusStopDetail")
    //                    if (Global.Form == "Route")
    //                    {
    //                        //latitude = Latitude;
    //                        //longitude = Longitude;
    //                        await CheckFences(Latitude, Longitude);
    //                    }
    //                    else if (Global.Form == "BusStopDetail")
    //                    {
    //                        if (!MainThread.IsMainThread)
    //                        {
    //                            MainThread.BeginInvokeOnMainThread(() =>
    //                            {
    //                                displayBusStop();
    //                            });
    //                        }
    //                        else
    //                        {
    //                            displayBusStop();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    if (Global.Form == "BusStopDetail")
    //                    {
    //                        if (!MainThread.IsMainThread)
    //                        {
    //                            MainThread.BeginInvokeOnMainThread(() =>
    //                            {
    //                                displayBusStop();
    //                            });
    //                        }
    //                        else
    //                        {
    //                            displayBusStop();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            longitude = "0";
    //            latitude = "0";
    //        }
    //    //}
    //}



    //async Task startGPS()
    //{
    //    //if (Device.RuntimePlatform == Device.Android)
    //    //{
    //    // Probably Invoke
    //        MessagingCenter.Subscribe<LocationMessage>(this, "Location", message => {
    //            MainThread.BeginInvokeOnMainThread(async () => {
    //                await GpsPositionChanged(message.Speed, message.Latitude.ToString(), message.Longitude.ToString());
    //                //    locationLabel.Text += $"{Environment.NewLine}{message.Latitude}, {message.Longitude}, {DateTime.Now.ToLongTimeString()}";

    //                Console.WriteLine($"{message.Latitude}, {message.Longitude}, {DateTime.Now.ToLongTimeString()}");
    //            });
    //        });

    //    MessagingCenter.Subscribe<StopServiceMessage>(this, "ServiceStopped", message =>
    //    {
    //        MainThread.BeginInvokeOnMainThread(() =>
    //        {
    //            Application.Current.MainPage.DisplayAlert("Alert", "Location Service has been stopped!", "OK");
    //            //locationLabel.Text = "Location Service has been stopped!";
    //        });
    //    });

    //    MessagingCenter.Subscribe<LocationErrorMessage>(this, "LocationError", message =>
    //    {
    //        MainThread.BeginInvokeOnMainThread(() =>
    //        {
    //            Application.Current.MainPage.DisplayAlert("Alert", "There was an error subscribing to location updates!", "OK");
    //        });
    //    });


    //    //if (Preferences.Get("LocationServiceRunning", false) == false)
    //    if (string.IsNullOrEmpty(Global.GpsStatus))
    //    {
    //        StartGPSService();
    //    }
    //}

    public void StartGPSService(CancellationToken token)
    {
        try
        {
            var startServiceMessage = new StartServiceMessage();
            MessagingCenter.Send(startServiceMessage, "ServiceStarted");
            Global.GpsStatus = "Connected";
            Global.isCheckingLocation = true;

            //status = GpsStatus.Connected;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        //Preferences.Set("LocationServiceRunning", true);
        //try
        //{
        //Global.cts = new CancellationTokenSource();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}

        Task.Run(async () => {
            try
            {
                var gpsService = new GetLocationService();
                //gpsService.Run(token).Wait();
                await gpsService.Run(token);
            }
            //catch (Android.OS.OperationCanceledException)
            catch (Exception ex)
            {
            }
            finally
            {
                if (Global.GPSTokenSource.IsCancellationRequested)
                {
                    StopGPSService();
                    //var message = new StopServiceMessage();
                    //Global.GpsStatus = "";
                    ////status = GpsStatus.Disabled;
                    //Device.BeginInvokeOnMainThread(
                    //    () => MessagingCenter.Send(message, "ServiceStopped")
                    //);
                }
            }
        }, token);


        //locationLabel.Text = "Location Service has been started!";
    }

    public void StopGPSService()
    {
        //TODO Need to actually stop the service.
        //_cts.Cancel();//.IsCancellationRequested = true;

        if (Global.GpsStatus == "Connected" && Global.GPSTokenSource != null && Global.GPSTokenSource.IsCancellationRequested == false)
        {
            //
            Global.GPSTokenSource.Cancel();
        }

        //CancelRequest();
        var stopServiceMessage = new StopServiceMessage();
        MessagingCenter.Send(stopServiceMessage, "ServiceStopped");
        //Preferences.Set("LocationServiceRunning", false);
        //Global.GpsStatus = "";
        //status = GpsStatus.Disabled;
    }

    //private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
    //{
    //    locationLabel.Text += $"{e.Position.Latitude}, {e.Position.Longitude}, {e.Position.Timestamp.TimeOfDay}{Environment.NewLine}";

    //    Console.WriteLine($"{e.Position.Latitude}, {e.Position.Longitude}, {e.Position.Timestamp.TimeOfDay}");
    //}

    //private static CancellationTokenSource _cancelTokenSource;
    ////private static bool _isCheckingLocation;

    //public async Task GetCurrentLocation()
    //{
    //    try
    //    {
    //        Global.isCheckingLocation = true;

    //        GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

    //        _cancelTokenSource = new CancellationTokenSource();

    //        Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

    //        if (location != null)
    //            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
    //    }
    //    // Catch one of the following exceptions:
    //    //   FeatureNotSupportedException
    //    //   FeatureNotEnabledException
    //    //   PermissionException
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        // Unable to get location
    //    }
    //    finally
    //    {
    //        Global.isCheckingLocation = false;
    //    }
    //}

    //public void CancelRequest()
    //{
        
    //    //if (Global.isCheckingLocation && Global.GPSTokenSource != null &&
    //    if (Global.GpsStatus == "Connected" && Global.GPSTokenSource != null && Global.GPSTokenSource.IsCancellationRequested == false)
    //        Global.GPSTokenSource.Cancel();
    //}
}
