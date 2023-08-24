using System;
using System.Collections.Generic;
using RdId.Core.Factories;
using RfId.Core.Enums;
using RfId.Core.Models.ConnectionInfo;
using RfId.Core.RfIdClients;
using RfId.Models.Events;

namespace RfId.Core
{
    public class RfIdService
    {
        //singleton
        //static GpsService instance;

        #region Private Properties

        private readonly BaseRfIdClient _client;

        #endregion

        #region Public Properties

        public bool IsRunning => _client.IsRunning;
        public RfIdType GpsType => _client.RfIdType;

        public List<EventHandler<RfIdDataEventArgs>> RfIdCallbackEventdelegates = new List<EventHandler<RfIdDataEventArgs>>();
        public List<EventHandler<string>> RawRfIdCallbackEventdelegates = new List<EventHandler<string>>();
        public List<EventHandler<RfIdStatus>> RfIdStatusEventdelegates = new List<EventHandler<RfIdStatus>>();

        #endregion

        #region Constructors

        public RfIdService(BaseRfIdInfo baseRfIdData)
        {
            _client = RfIdClientFactory.Create(baseRfIdData);
        }

        public RfIdService(RfIdType RfIdServiceType)
        {
            _client = RfIdClientFactory.Create(RfIdServiceType);
        }

        #endregion

        #region Connect and Disconnect

        public bool Connect()
        {
            return _client.Connect();
        }

        public bool Disconnect()
        {
            return _client.Disconnect();
        }

        #endregion

        #region Register Events

        public void RegisterDataEvent(Action<object, RfIdDataEventArgs> action)
        {
            _client.RfIdCallbackEvent += new EventHandler<RfIdDataEventArgs>(action);
            RfIdCallbackEventdelegates.Add(new EventHandler<RfIdDataEventArgs>(action));
        }

        public void RegisterRawDataEvent(Action<object, string> action)
        {
            _client.RawRfIdCallbackEvent += new EventHandler<string>(action);
            RawRfIdCallbackEventdelegates.Add(new EventHandler<string>(action));
        }

        public void RegisterStatusEvent(Action<object, RfIdStatus> action)
        {
            _client.RfIdStatusEvent += new EventHandler<RfIdStatus>(action);
            RfIdStatusEventdelegates.Add(new EventHandler<RfIdStatus>(action));
        }

        public void RemoveDataEvent(Action<object, RfIdDataEventArgs> action)
        {
            _client.RfIdCallbackEvent -= new EventHandler<RfIdDataEventArgs>(action);
        }

        public void RemoveRawDataEvent(Action<object, string> action)
        {
            _client.RawRfIdCallbackEvent -= new EventHandler<string>(action);
        }

        public void RemoveStatusEvent(Action<object, RfIdStatus> action)
        {
            _client.RfIdStatusEvent -= new EventHandler<RfIdStatus>(action);
        }

        public void ResetRfIdCallbackEvent()
        {
            //GpsCallbackEventdelegates
            while (RfIdCallbackEventdelegates.Count > 1)
            {
                var eh = RfIdCallbackEventdelegates[0];
                _client.RfIdCallbackEvent -= eh;
            }
        }

        public void ResetRawDataEvent()
        {
            while (RawRfIdCallbackEventdelegates.Count > 1)
            {
                var eh = RawRfIdCallbackEventdelegates[0];
                _client.RawRfIdCallbackEvent -= eh;
            }
        }

        public void ResetStatusEvent()
        {
            while (RfIdStatusEventdelegates.Count > 1)
            {
                var eh = RfIdStatusEventdelegates[0];
                _client.RfIdStatusEvent -= eh;
            }
        }
        #endregion
    }
}