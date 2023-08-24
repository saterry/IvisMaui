using IvisMaui.Models;

namespace IvisMaui.Services
{
    public class StatusQueueService : IStatusQueueService
    {
        IRestService _restService;

        public StatusQueueService(IRestService service)
        {
            _restService = service;
        }

        //public Task<List<StatusQueue>> GetTasksAsync()
        //{
        //    return _restService.RefreshDataAsync();
        //}

        public Task<bool> SendTaskAsync(StatusQueue item)
        {
            return _restService.SendStatusQueueAsync(item);
        }

        //public Task DeleteTaskAsync(TodoItem item)
        //{
        //    return _restService.DeleteTodoItemAsync(item.ID);
        //}
    }
}
