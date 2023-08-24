using System;

namespace GPS.Models.Events
{
    public class GpsDataEventArgs : EventArgs
    {

        public double Speed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public GpsDataEventArgs(double Speed, double Latitude, double Longitude)
        {
            this.Speed = Speed;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }

        public GpsDataEventArgs()
        {
        }

    }
}
