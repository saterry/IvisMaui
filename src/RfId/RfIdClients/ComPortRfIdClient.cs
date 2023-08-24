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

    public class ComPortRfIdClient : BaseRfIdClient
    {

        //private Boolean boCONN = false,
        //                boThreadStart = false,
        //                boFunctionOn = false;


        //private byte bKeyIndex = 0;
        //private byte bTypeOfCard = 0;
        //private string[] ERROR_CODES;


        #region Private Properties

        //private readonly NmeaParser _parser = new NmeaParser();

#if DEBUG
        private SerialPort _serialPort;
#endif
        //private DateTime? _previousReadTime;

        #endregion

        #region Constructors

        public ComPortRfIdClient(ComPortInfo connectionData) : base(RfIdType.ComPort, connectionData)
        {
        }

        public ComPortRfIdClient(BaseRfIdInfo connectionData) : base(RfIdType.ComPort, connectionData)
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
            //_serialPort = new SerialPort(data.ComPort, 4800, Parity.None, 8, StopBits.One);
            _serialPort = new SerialPort(data.ComPort, 9600, Parity.None, 8, StopBits.One);
            //_serialPort = new SerialPort(data.ComPort, 19200, Parity.None, 8, StopBits.One);

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

        #region Location Callbacks

#if DEBUG
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var readString = _serialPort.ReadExisting();

                //var readString = ReadDigitalLogic();

                OnRawRfIdDataReceived(readString);
                //var result = _parser.Parse(readString);
                //if (result == null) return;     //Console.WriteLine("No $");
                //if (typeof(GprmcMessage) != result.GetType()) return;
                //if (_previousReadTime != null && GpsInfo.ReadFrequenty != 0 && ((GprmcMessage)result).UpdateDate.Subtract(new TimeSpan(0, 0, 0, 0, GpsInfo.ReadFrequenty)) <= _previousReadTime) return;
                OnRfIdDataReceived(new RfIdDataEventArgs(readString));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
#endif
#endregion
    }
}