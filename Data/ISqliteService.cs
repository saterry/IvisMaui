using IvisMaui.Models;

namespace IvisMaui.Data
{
    public interface ISqliteService
    {
        Task CreateStudentTable();

        Task AddStudent(Student student);

        Task<List<Student>> GetAllStudents();

        Task<List<Student>> LoadStudents(string BusId);

        Task<Student> LoadStudent(string StudentNumber);

        Task DeleteStudent(int id);

        Task<List<Bus>> BusListAsync();

        List<Bus> BusList();

        Task<List<BusSelect>> BusSelectList();

        Task<List<Busmessage>> MessageList(string BusId);

        Task<District> LoadDistrict();

        Task<List<Route>> LoadRoutes(string BusId);

        Task<Studentstopdetail> GetStopIdForStudentCardInRoute(string CardNumber, string RouteId, string AmPm);

        Task<Busstop> GetBusStopByStopIdInRoute(string StopId, string RouteId, string AmPm);

        Task<Studentstopdetail> LoadStudentStopDetail(string StudentNumber, string StopId, string AmPm);

        Task<List<Busstop>> LoadGeoFenceList();

        Task<List<Busstop>> LoadBusStops(string RouteId, string BusId, string AmPm);

        Task<List<Studentstopdetail>> LoadStudentStopDetails(string StopId, string AmPm);

        Task<List<Studentstopdetail>> LoadStudentOnBusDetails();

        Task<List<Studentstopdetail>> LoadStudentBoardingBusDetails(string RouteId);

        Task<List<StatusQueue>> GetUploadStatusQueue();

        Task<List<StatusQueue>> LoadStatusQueue();


        Task SaveStatus(string BusNumber, string RouteId, string StopId, string StudentNumber, int Status, string AmPm);

        Task UnloadBus(string BusNumber);
    }
}
