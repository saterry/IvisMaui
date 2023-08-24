using GPS.Models.Events;
using IvisMaui.GPS.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IvisMaui.Services
{


    public class GetLocationService
	{

        #region Event handlers

		//public event EventHandler<GpsDataEventArgs> GpsCallbackEvent;
        //public event EventHandler<string> RawGpsCallbackEvent;
        public event EventHandler<GpsStatus> GpsStatusEvent;

        #endregion


        #region Public Properties

        //public List<EventHandler<GpsDataEventArgs>> GpsCallbackEventdelegates = new List<EventHandler<GpsDataEventArgs>>();
        //public List<EventHandler<string>> RawGpsCallbackEventdelegates = new List<EventHandler<string>>();
        public List<EventHandler<GpsStatus>> GpsStatusEventdelegates = new List<EventHandler<GpsStatus>>();

        #endregion

        readonly bool stopping = false;
		public GetLocationService()
		{
		}

		public async Task Run(CancellationToken token)
		{
            await Task.Run(async () => {
				while (!stopping)
				{
					token.ThrowIfCancellationRequested();
					try
					{
						
						await Task.Delay(2000);

						MainThread.BeginInvokeOnMainThread(async () =>
						{
							var request = new GeolocationRequest(GeolocationAccuracy.Best);
							var location = await Geolocation.GetLocationAsync(request);
							if (location != null)
							{
								var message = new LocationMessage
								{
									Speed = location.Speed ?? 0,
									Latitude = location.Latitude,
									Longitude = location.Longitude
								};

								//MainThread.BeginInvokeOnMainThread(() =>
								//{
								MessagingCenter.Send(message, "Location");
								//});
							}
						});
					}
					catch (Exception ex)
					{
                        MainThread.BeginInvokeOnMainThread(() =>
						{
							var errormessage = new LocationErrorMessage();
							MessagingCenter.Send(errormessage, "LocationError");
						});
					}
				}
				return;
			}, token);
		}

        #region Register Events

        public void RegisterStatusEvent(Action<object, GpsStatus> action)
        {
            GpsStatusEvent += new EventHandler<GpsStatus>(action);
            GpsStatusEventdelegates.Add(new EventHandler<GpsStatus>(action));
        }

        //public void RegisterDataEvent(Action<object, GpsDataEventArgs> action)
        //{
        //    GpsCallbackEvent += new EventHandler<GpsDataEventArgs>(action);
        //    GpsCallbackEventdelegates.Add(new EventHandler<GpsDataEventArgs>(action));
        //}

        #endregion
    }
}