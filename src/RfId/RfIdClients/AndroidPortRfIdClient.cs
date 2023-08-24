using System;
using System.Threading;
using IO.Ports;
using IO.Ports.Core;
using RfId.Core.Enums;
using RfId.Core.Models.ConnectionInfo;
using RfId.Core.Exceptions;
using RfId.Models.Events;

namespace RfId.Core.RfIdClients
{
    public class AndroidPortRfIdClient : BaseRfIdClient
    {
        #region Private Properties

        //private readonly NmeaParser _parser = new NmeaParser();
        private SerialPort _serialPort;
        //private DateTime? _previousReadTime;

        #endregion

        #region Constructors

        public AndroidPortRfIdClient(RaspiPortInfo connectionData) : base(RfIdType.ComPort, connectionData)
        {
        }

        public AndroidPortRfIdClient(BaseRfIdInfo connectionData) : base(RfIdType.ComPort, connectionData)
        {
        }

        #endregion

        #region Connect and Disconnect

        public override bool Connect()
        {
            var data = (RaspiPortInfo)RfIdInfo;

            IsRunning = true;
            OnRfIdStatusChanged(RfIdStatus.Connecting);
            //_serialPort = new Bifrost.IO.Ports.SerialPort(data.ComPort, 9600);

            _serialPort = new SerialPort()
            {
                PortName = "/dev/ttyACM0",
                BaudRate = 9600
            };


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
            catch
            {
                Disconnect();
                //throw;
                throw new NoRfIdFoundException();
            }

            return true;
        }

        public override bool Disconnect()
        {
            _serialPort.Close();
            IsRunning = false;
            OnRfIdStatusChanged(RfIdStatus.Disabled);
            return true;
        }

        #endregion

        #region Location Callbacks
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //Console.WriteLine("1");
                var readString = _serialPort.ReadExisting();
                OnRawRfIdDataReceived(readString);
                //var result = _parser.Parse(readString);
                //Console.WriteLine("2");
                //if (result == null) return;     //Console.WriteLine("No $");
                //if (typeof(GprmcMessage) != result.GetType()) return;
                //if (_previousReadTime != null && GpsInfo.ReadFrequenty != 0 && ((GprmcMessage)result).UpdateDate.Subtract(new TimeSpan(0, 0, 0, 0, GpsInfo.ReadFrequenty)) <= _previousReadTime) return;
                //Console.WriteLine("3");
                OnRfIdDataReceived(new RfIdDataEventArgs(readString));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}