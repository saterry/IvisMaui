using Ghostware.GPS.NET;
using Ghostware.GPS.NET.Enums;
using Ghostware.GPS.NET.Exceptions;
using Ghostware.GPS.NET.Models.ConnectionInfo;
using Ghostware.GPS.NET.Models.Events;
using System;
using System.Threading;
using IvisMaui.GPS.Model;
using IvisMaui.Data;
using System.Collections.Generic;

namespace IvisMaui.GPS
{
    public class GPS
    {
        //singleton
        public static GPS instance;

        #region Constructors
        //public GPS(Action<string> write, CancellationToken token)
        //public GPS(Action<string> write, CancellationToken token)
        public GPS(Action<string> write)
        {
            this.write = write;
            //this.token = token;
        }

        public GPS()
        {
        }

        public static GPS Instance()
        {
            if (instance == null)
            {
                instance = new GPS();
            }
            return instance;
        }

        #endregion

        private readonly Action<string> write;
        private CancellationToken token;
        public GPSPositionChangedEventArgs gpsPosition = new GPSPositionChangedEventArgs();
        public static GpsService _gpsService;
        public GpsStatus status = GpsStatus.Disabled;
        public static int counter = 0;
        public event EventHandler<GpsDataEventArgs> GpsCallbackEvent;
        public List<EventHandler<GpsDataEventArgs>> GpsCallbackEventdelegates = new List<EventHandler<GpsDataEventArgs>>();

        public event EventHandler<GpsStatus> GpsStatusEvent;
        public List<EventHandler<GpsStatus>> GpsStatusEventdelegates = new List<EventHandler<GpsStatus>>();

        public void RegisterPositionChangedEvent(Action<object, GpsDataEventArgs> action)
        {
            GpsCallbackEvent += new EventHandler<GpsDataEventArgs>(action);
            GpsCallbackEventdelegates.Add(new EventHandler<GpsDataEventArgs>(action));
        }

        public void RegisterStatusChangedEvent(Action<object, GpsStatus> action)
        {
            GpsStatusEvent += new EventHandler<GpsStatus>(action);
            GpsStatusEventdelegates.Add(new EventHandler<GpsStatus>(action));
        }

        public void RemovePositionChangedEvent(Action<object, GpsDataEventArgs> action)
        {
            GpsCallbackEvent -= new EventHandler<GpsDataEventArgs>(action);
            GpsCallbackEventdelegates.Remove(new EventHandler<GpsDataEventArgs>(action));
        }

        public void ResetPositionChangedEvent()
        {
            foreach (EventHandler<GpsDataEventArgs> eh in GpsCallbackEventdelegates)
            {
                GpsCallbackEvent -= eh;
            }
            GpsCallbackEventdelegates.Clear();
        }

        public void StopGPS()
        {
            if (Global.TokenSource != null)
            {
                //_gpsService.RemoveStatusEvent(Action);
                //_gpsService.RemoveDataEvent(GpsServiceOnLocationChanged);
                Global.TokenSource.Cancel();
            }
        }

        public void StartGPS(CancellationToken token)
        {
            this.token = token;
#if DEBUG
            var info = new ComPortInfo()
            {
                ComPort = "COM4\0\0\0\0",
                //ComPort = "COM3\0\0\0\0",
            };
#else
            var info = new RaspiPortInfo()
            {
                ComPort = "/dev/ttyACM0",
                //ComPort = "/dev/serial0",
            };
#endif
            _gpsService = new GpsService(info);
            _gpsService.RegisterStatusEvent(Action);
            _gpsService.RegisterDataEvent(GpsServiceOnLocationChanged);

            try
            {
                _gpsService.Connect();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("The selected com port is already in use!");
            }
            catch (NoGpsFoundException)
            {
                Console.WriteLine("No GPS found");
            }
        }

        private void Action(object o, GpsStatus gpsStatus)
        {
            status = gpsStatus;
            GpsStatusEvent?.Invoke(this, status);
            Console.WriteLine(gpsStatus);
            if (write != null)
            {
                write(gpsStatus.ToString());
            }
        }

        private void GpsServiceOnLocationChanged(object sender, GpsDataEventArgs e)
        {
            //maybe check status?

            counter = counter + 1;
            //Console.WriteLine($"[{counter}]");
            //latitude = e.Latitude;
            //longitude = e.Longitude;
            //speed = e.Speed;
            //write(e.ToString());
            if (write != null)
            {
                write($"[{counter}]" + e.ToString());
            }
            else
            {
                gpsPosition.Position.Latitude = e.Latitude;
                gpsPosition.Position.Longitude = e.Longitude;
                gpsPosition.Position.Speed = e.Speed;

                GpsCallbackEvent?.Invoke(this, e);
                //writePosition(gpsPosition);
            }
            if (token.IsCancellationRequested)
            {
                if (write != null)
                {
                    write("Cancel logging.");
                }
                _gpsService.Disconnect();
            }
        }

        //public class GPSLocationChangedEventArgs : EventArgs
        //{
        //    public GPSLocationChangedEventArgs(GPSLocation location)
        //    {
        //        Status = status;
        //    }

        //    public GPSLocation Location { get; private set; }
        //}

        public class GPSPositionChangedEventArgs : EventArgs
        {
            public GPSPositionChangedEventArgs()
            {
                Position = new GPSPosition();
            }

            //public GPSPositionChangedEventArgs(GPSPosition position, string status, string location)
            //{
            //    Position = position;
            //    Status = status;
            //    Location = location;
            //}

            public GPSPosition Position { get; private set; }
            public string Status { get; private set; }
            public string Location { get; private set; }
        }
    }
}
