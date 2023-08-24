using IvisMaui.Models;

namespace IvisMaui.Services
{
    public interface IRestService
    {
        //Task<List<StatusQueue>> RefreshDataAsync();

        Task<bool> SendStatusQueueAsync(StatusQueue item);

        //Task DeleteTodoItemAsync(string id);
    }
}
