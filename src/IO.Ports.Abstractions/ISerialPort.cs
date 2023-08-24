using IO.Ports.Core;
using System;

namespace IO.Ports.Abstractions
{
    public interface ISerialPort : IDisposable
    {
        int BaudRate { get; set; }

        string PortName { get; set; }

        bool IsOpen { get; set; }

        string ReadExisting();

        void Open();
        
        event SerialDataReceivedEventHandler DataReceived;

        void Close();
    }
}
