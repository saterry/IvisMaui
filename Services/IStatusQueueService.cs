using IvisMaui.Models;

namespace IvisMaui.Services
{
    public interface IStatusQueueService
    {
        //Task<List<TodoItem>> GetTasksAsync();
        Task<bool> SendTaskAsync(StatusQueue item);
        //Task DeleteTaskAsync(TodoItem item);
    }
}
