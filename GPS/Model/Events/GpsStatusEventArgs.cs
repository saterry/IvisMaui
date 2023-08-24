using System;
//using Android.Locations;
using IvisMaui.GPS.Enums;

namespace GPS.Model.Events
{
    public class GpsStatusEventArgs : EventArgs
    {
        public GpsStatus Status { get; set; }

        public GpsStatusEventArgs()
        {
            
        }

        public GpsStatusEventArgs(GpsStatus status)
        {
            Status = status;
        }
    }
}
