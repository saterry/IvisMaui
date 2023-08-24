using System;
//using RfId.Core.Enums;
//using RfId.Models.Events;
//using RfId.Core.Models.ConnectionInfo;
//using RfId.Core.Exceptions;
//using RfId.Core;
//using Android.App;
//using Android.Content;
//using Android.Hardware.Usb;

//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace IvisMaui.Rfid
{
    public class RfId
    {
        //singleton
        public static RfId instance;

        #region Constructors
        public RfId(Action<string> write)
        {
            this.write = write;
        }

        public RfId()
        {
        }

        public static RfId Instance()
        {
            if (instance == null)
            {
                instance = new RfId();
            }
            return instance;
        }

        #endregion

        private readonly Action<string> write;
        private CancellationToken token;
//        public static RfIdService _rfidService;
//        public RfIdStatus status = RfIdStatus.Disabled;
//        public static int counter = 0;
//        public event EventHandler<RfIdDataEventArgs> RfIdCallbackEvent;
//        public List<EventHandler<RfIdDataEventArgs>> RfIdCallbackEventdelegates = new List<EventHandler<RfIdDataEventArgs>>();

//        public event EventHandler<RfIdStatus> RfIdStatusEvent;
//        public List<EventHandler<RfIdStatus>> RfIdStatusEventdelegates = new List<EventHandler<RfIdStatus>>();

//        public void RegisterDataChangedEvent(Action<object, RfIdDataEventArgs> action)
//        {
//            RfIdCallbackEvent += new EventHandler<RfIdDataEventArgs>(action);
//            RfIdCallbackEventdelegates.Add(new EventHandler<RfIdDataEventArgs>(action));

//        }

//        public void RegisterStatusChangedEvent(Action<object, RfIdStatus> action)
//        {
//            RfIdStatusEvent += new EventHandler<RfIdStatus>(action);
//            RfIdStatusEventdelegates.Add(new EventHandler<RfIdStatus>(action));
//        }

//        public void ResetDataChangedEvent()
//        {
//            foreach (EventHandler<RfIdDataEventArgs> eh in RfIdCallbackEventdelegates)
//            {
//                RfIdCallbackEvent -= eh;
//            }
//            RfIdCallbackEventdelegates.Clear();
//        }

//        public void StartRfId(CancellationToken token)
//        {
//            this.token = token;
////#if DEBUG
////            var info = new ComPortInfo()
////            {
////                ComPort = "COM7\0\0\0\0",
////                //ComPort = "COM4\0\0\0\0",
////            };
////#else
//            var info = new RaspiPortInfo()
//            {
//                ComPort = "/dev/ttyACM1",
//                //ComPort = "/dev/ttyS0",
//                //ComPort = "/dev/serial1",
//            };
////#endif
//            _rfidService = new RfIdService(info);
//            _rfidService.RegisterStatusEvent(Action);
//            _rfidService.RegisterDataEvent(RfIdServiceOnDataChanged);

//            try
//            {
//                _rfidService.Connect();
//            }
//            catch (UnauthorizedAccessException)
//            {
//                Console.WriteLine("The selected com port is already in use!");
//            }
//            catch (NoRfIdFoundException)
//            {
//                Console.WriteLine("No RfId device found");
//            }
//        }

//        private void Action(object o, RfIdStatus rfidStatus)
//        {
//            status = rfidStatus;
//            RfIdStatusEvent?.Invoke(this, status);
//            Console.WriteLine(rfidStatus);
//            if (write != null)
//            {
//                write(rfidStatus.ToString());
//            }
//        }

//        //private void RfIdServiceOnDataChanged(object sender, RfIdDataEventArgs e)
//        private void RfIdServiceOnDataChanged(object sender, RfIdDataEventArgs e)
//        {
//            //maybe check status?

//            counter = counter + 1;
//            if (write != null)
//            {
//                write($"[{counter}]" + e.ToString());
//            }
//            else
//            {
//                RfIdCallbackEvent?.Invoke(this, e);
//            }
//            if (token.IsCancellationRequested)
//            {
//                if (write != null)
//                {
//                    write("Cancel logging.");
//                }
//                _rfidService.Disconnect();
//            }
//        }

        //public bool temp()
        //{
        //    Activity act = Platform.CurrentActivity;

        //    UsbManager manager = (UsbManager)act.GetSystemService(Context.UsbService);

        //    IDictionary<string, UsbDevice> devicesDictionary = manager.DeviceList;

        //    UsbDevice dvc = devicesDictionary.ElementAt(0).Value;

        //    string ACTION_USB_PERMISSION = "rzepak";

        //    var interf = dvc.GetInterface(1);

        //    var outEndpoint = interf.GetEndpoint(1);

        //    PendingIntent mPermissionIntent = PendingIntent.GetBroadcast(act, 0, new Intent(ACTION_USB_PERMISSION), 0);
        //    IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);

        //    if (manager.HasPermission(dvc) == false) manager.RequestPermission(dvc, mPermissionIntent);

        //    var deviceConnection = manager.OpenDevice(dvc);

        //    if (deviceConnection != null) deviceConnection.ClaimInterface(interf, true).ToString();
        //    else return false;
        //}
    }
}