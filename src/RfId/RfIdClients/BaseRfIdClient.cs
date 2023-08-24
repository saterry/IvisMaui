using System;
//using Ghostware.GPS.NET.Converters;
//using Ghostware.GPS.NET.Enums;
using RfId.Core.Enums;
using RfId.Core.Models.ConnectionInfo;
using RfId.Models.Events;
//using Ghostware.GPS.NET.Models.ConnectionInfo;
//using Ghostware.GPS.NET.Models.Events;

namespace RfId.Core.RfIdClients
{
    public abstract class BaseRfIdClient
    {
        #region Properties

        public RfIdType RfIdType { get; }
        public bool IsRunning { get; set; }

        protected BaseRfIdInfo RfIdInfo { get; set; }

        #endregion
        
        #region Event handlers

        public event EventHandler<RfIdDataEventArgs> RfIdCallbackEvent;
        public event EventHandler<string> RawRfIdCallbackEvent;
        public event EventHandler<RfIdStatus> RfIdStatusEvent;

        #endregion

        #region Constructors

        protected BaseRfIdClient(RfIdType rfidType, BaseRfIdInfo rfidInfo)
        {
            RfIdType = rfidType;
            RfIdInfo = rfidInfo;
        }

        #endregion

        #region Connect and Disconnect

        public abstract bool Connect();

        public abstract bool Disconnect();

        #endregion

        #region Events Triggers


        protected virtual void OnRfIdDataReceived(RfIdDataEventArgs e)
        {
            //if (GpsInfo.CoordinateSystem == GpsCoordinateSystem.Lambert72)
            //{
                //var x = 0.0d;
                //var y = 0.0d;
                //var h = 0.0d;
                //CoordinateConverterUtilities.GeoETRS89ToLambert72(e.Latitude, e.Longitude, 0, ref x, ref y, ref h);
                //e.CoordinateSystem = GpsCoordinateSystem.Lambert72;
                //e.Latitude = x;
                //e.Longitude = y;
            //}

            RfIdCallbackEvent?.Invoke(this, e);
        }

        protected virtual void OnRawRfIdDataReceived(string e)
        {
            RawRfIdCallbackEvent?.Invoke(this, e);
        }

        protected virtual void OnRfIdStatusChanged(RfIdStatus e)
        {
            RfIdStatusEvent?.Invoke(this, e);
        }

        #endregion
    }
}