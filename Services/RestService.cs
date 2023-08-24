using System.Diagnostics;
using System.Text;
using System.Text.Json;
using IvisMaui.Models;
using IvisMaui.Data;

namespace IvisMaui.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        //IHttpsClientHandlerService _httpsClientHandlerService;

        public List<StatusQueue> Items { get; private set; }

        //public RestService(IHttpsClientHandlerService service)
        public RestService()
        {
#if DEBUG
            //_httpsClientHandlerService = service;
            //HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            //if (handler != null)
            //    _client = new HttpClient(handler);
            //else
            _client = new HttpClient();
#else
            _client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        //public async Task<List<StatusQueue>> RefreshDataAsync()
        //{
        //    Items = new List<StatusQueue>();

        //    Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
        //    try
        //    {
        //        HttpResponseMessage response = await _client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string content = await response.Content.ReadAsStringAsync();
        //            Items = JsonSerializer.Deserialize<List<StatusQueue>>(content, _serializerOptions);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }

        //    return Items;
        //}

        public async Task<bool> SendStatusQueueAsync(StatusQueue item)
        {
            // uri = new Uri(Constants.RestUrl);

            HttpResponseMessage response = null;
            try
            {
                History history= new History();
                history.DistrictId = item.DistrictId;
                history.BusNumber = item.BusNumber;
                history.RouteId = item.RouteId;
                history.AmPm = item.AmPm;
                history.Status = item.Status;
                history.StudentNumber = item.StudentNumber;
                history.StatusDate = DateTime.Parse(item.StatusDate);

                string json = JsonSerializer.Serialize<History>(history, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                //if (isNewItem)
                response = await _client.PostAsync(Constants.RestUrl, content);
                //else
                //    response = await _client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tStatusQueue successfully saved.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return response.IsSuccessStatusCode;
        }

        //public async Task DeleteTodoItemAsync(string id)
        //{
        //    Uri uri = new Uri(string.Format(Constants.RestUrl, id));

        //    try
        //    {
        //        HttpResponseMessage response = await _client.DeleteAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //            Debug.WriteLine(@"\tTodoItem successfully deleted.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }
        //}
    }
}