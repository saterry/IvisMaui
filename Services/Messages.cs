using System;
namespace IvisMaui.Services
{
    public class StartServiceMessage
    {
    }

    public class StopServiceMessage
    {
    }

    public class LocationMessage
    {
        public double Speed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class LocationErrorMessage
    {
    }
}