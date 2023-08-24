//using RfId.Core.Enums;
using IvisMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IvisMaui.Data
{
    static class Global
    {
        private static Bus _bus = new Bus();
        private static Route _route = new Route();
        private static Busstop _busstop = new Busstop();
        private static string _init = "";
        private static string _busNumber = "";
        private static string _busId = "0";
        private static string _routeId = "";
        private static string _AmPm = "";
        private static string _Intersection1 = "";
        private static string _Intersection2 = "";
        private static string _StopId = "";
        private static string _Form = "";
        private static string _GpsStatus = "";
        private static string _RfIdStatus = "";
        private static string _PrevCardNumber;
        private static bool _Connected;
        private static bool _isStopped = false;
        //private static CancellationTokenSource _cts;
        private static bool _isCheckingLocation;
        //private static string _RfIdDisplayStatus = "";
        //private static string _Conn = "";
        private static CancellationTokenSource _TokenSource = null;
        private static CancellationTokenSource _RfIdTokenSource = null;
        private static CancellationTokenSource _GPSTokenSource = null;
        private static string _connectionString = "";

        public static Bus Bus
        {
            get { return _bus; }
            set { _bus = value; }
        }

        public static Route Route
        {
            get { return _route; }
            set { _route = value; }
        }

        public static Busstop Busstop
        {
            get { return _busstop; }
            set { _busstop = value; }
        }

        public static string BusNumber
        {
            get { return _busNumber; }
            set { _busNumber = value; }
        }
        public static string BusId
        {
            get { return _busId; }
            set { _busId = value; }
        }

        public static string Init
        {
            get { return _init; }
            set { _init = value; }
        }


        public static string RouteId
        {
            get { return _routeId; }
            set { _routeId = value; }
        }

        public static string StopId
        {
            get { return _StopId; }
            set { _StopId = value; }
        }

        public static string Intersection1
        {
            get { return _Intersection1; }
            set { _Intersection1 = value; }
        }

        public static string Intersection2
        {
            get { return _Intersection2; }
            set { _Intersection2 = value; }
        }

        public static string AmPm
        {
            get { return _AmPm; }
            set { _AmPm = value; }
        }

        public static string Form
        {
            get { return _Form; }
            set { _Form = value; }
        }
        public static string GpsStatus
        {
            get { return _GpsStatus; }
            set { _GpsStatus = value; }
        }

        public static string RfIdStatus
        {
            get { return _RfIdStatus; }
            set { _RfIdStatus = value; }
        }
        public static string PrevCardNumber
        {
            get { return _PrevCardNumber; }
            set { _PrevCardNumber = value; }
        }

        //public static string Conn
        //{
        //    get { return _Conn; }
        //    set { _Conn = value; }
        //}

        public static CancellationTokenSource TokenSource
        {
            get { return _TokenSource; }
            set { _TokenSource = value; }
        }

        public static CancellationTokenSource RfIdTokenSource
        {
            get { return _RfIdTokenSource; }
            set { _RfIdTokenSource = value; }
        }
        public static CancellationTokenSource GPSTokenSource
        {
            get { return _GPSTokenSource; }
            set { _GPSTokenSource = value; }
        }

        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public static bool Connected
        {
            get { return _Connected; }
            set { _Connected = value; }
        }
        public static bool isStopped
        {
            get { return _isStopped; }
            set { _isStopped = value; }
        }


        public static bool isCheckingLocation
        {
            get { return _isCheckingLocation; }
            set { _isCheckingLocation = value; }
        }
        //public static CancellationTokenSource cts
        //{
        //    get { return cts; }
        //    set { _cts = value; }
        //}
    }
}
