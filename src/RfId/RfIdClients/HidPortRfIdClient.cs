using System;
#if DEBUG
using System.IO.Ports;
#endif

using System.Threading;
using RfId.Core.Enums;
using RfId.Core.Exceptions;
using RfId.Core.Models.ConnectionInfo;
using RfId.Models.Events;

namespace RfId.Core.RfIdClients
{
    using DL_STATUS = System.UInt32;

    public class HidPortRfIdClient : BaseRfIdClient
    {


        #region Private Properties
#if DEBUG
        private SerialPort _serialPort;
#endif


        #endregion

        #region Constructors

        public HidPortRfIdClient(ComPortInfo connectionData) : base(RfIdType.HidPort, connectionData)
        {
        }

        public HidPortRfIdClient(BaseRfIdInfo connectionData) : base(RfIdType.HidPort, connectionData)
        {
        }

        #endregion

        #region Connect and Disconnect

        public override bool Connect()
        {
            var data = (ComPortInfo)RfIdInfo;
#if DEBUG

            IsRunning = true;
            OnRfIdStatusChanged(RfIdStatus.Connecting);
            _serialPort = new SerialPort(data.ComPort, 9600, Parity.None, 8, StopBits.One);

            // Attach a method to be called when there
            // is data waiting in the port's buffer
            _serialPort.DataReceived += port_DataReceived;
            try
            {
                // Begin communications
                _serialPort.Open();

                OnRfIdStatusChanged(RfIdStatus.Connected);
                // Enter an application loop to keep this thread alive
                while (_serialPort.IsOpen)
                {
                    Thread.Sleep(data.ReadFrequenty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
                //throw;
                throw new NoRfIdFoundException();
            }
#endif

            return true;
        }

        public override bool Disconnect()
        {
#if DEBUG
            _serialPort.Close();
#endif
            IsRunning = false;
            OnRfIdStatusChanged(RfIdStatus.Disabled);
            return true;
        }

        #endregion

        #region Digital Logic

        #endregion

        #region Location Callbacks
#if DEBUG
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var readString = _serialPort.ReadExisting();

                OnRawRfIdDataReceived(readString);
                OnRfIdDataReceived(new RfIdDataEventArgs(readString));
            }
            catch (Exception )
            {
                //Console.WriteLine(ex.Message);
            }
        }
        #endif

        #endregion
    }
}