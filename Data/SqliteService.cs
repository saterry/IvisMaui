using IvisMaui.Models;
using IvisMaui.Services;
//using Java.Net;
//using Microsoft.VisualBasic;
//using Org.Apache.Http;
using SQLite;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Json;

//https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/DatabaseAsync-sqlite?view=net-maui-7.0

namespace IvisMaui.Data
{
    public class SqliteService : ISqliteService
    {
        SQLiteAsyncConnection DatabaseAsync;
        SQLiteConnection Database;
        IStatusQueueService _statusqueueService;

        //string _dbPath;
        //private SQLiteConnection conn;

        //public SqliteService(string dbPath)
        public SqliteService(IStatusQueueService statusqueueService)
        {
            _statusqueueService = statusqueueService;
            //_dbPath = dbPath;
        }

        void InitAsync()
        {
            if (DatabaseAsync is not null)
                return;

            DatabaseAsync = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            //var result = await DatabaseAsync.CreateTableAsync<TodoItem>();
        }

        void Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
        }


        public async Task CreateStudentTable()
        {
            InitAsync();
            var result = await DatabaseAsync.CreateTableAsync<Student>();
            //try
            //{
            //    conn = new SQLiteConnection(_dbPath);
            //    conn.CreateTable<Student>();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        public async Task AddStudent(Student student)
        {
            InitAsync();
            await DatabaseAsync.InsertAsync(student);
            //conn = new SQLiteConnection(_dbPath);
            //conn.Insert(student);
        }

        public async Task<List<Student>> GetAllStudents()
        {
            InitAsync();
            try
            {
                await CreateStudentTable();
                return await DatabaseAsync.Table<Student>().ToListAsync();//.Where(t => t.Done).ToListAsync();
                //return conn.Table<Student>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<Student>();
        }

        public async Task<List<Student>> LoadStudents(string BusId)
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            var output = await DatabaseAsync.QueryAsync<Student>("select distinct s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Intersection1, bs.Intersection2, x.Name as School " +
                "from Student s " +
                "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                "join Busstop bs on ss.StopId = bs.StopId " +
                "join School x on s.School = x.SchoolId " +
                "where bs.BusId = ? " +
                "order by LastName, FirstName", BusId);
            return output.ToList();
            //}
        }

        public async Task<Student> LoadStudent(string StudentNumber)
        {
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{

            //parameters.Add("@StudentNumber", StudentNumber, DbType.String, ParameterDirection.Input);
            InitAsync();
            var output = await DatabaseAsync.QueryAsync<Student>("select s.Id as Id, s.StudentNumber as StudentNumber, s.LastName as LastName, s.FirstName as FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone as Phone, s.Grade as Grade, s.DOB as DOB, s.Address as Address, s.City as City, s.State as State, s.Zip as Zip, bs.Intersection1, bs.Intersection2, x.Name as School " +
                "from Student s " +
                "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                "join Busstop bs on ss.StopId = bs.StopId " +
                "join School x on s.School = x.Id " +
                "where s.StudentNumber = ?", StudentNumber);
            return output.FirstOrDefault();
            //}
        }

        public async Task DeleteStudent(int id)
        {
            InitAsync();
            //conn = new SQLiteConnection(_dbPath);
            await DatabaseAsync.DeleteAsync(new Student { Id = id });
            //conn.Delete(new Student { Id = id });
        }

        public async Task<List<Bus>> BusListAsync()
        {
            InitAsync();
            List<Bus> output = new();
            try
            {
                output = await DatabaseAsync.QueryAsync<Bus>("select BusId, Number from Bus Order by Number");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return output.ToList();
        }

        public List<Bus> BusList()
        {
            Init();
            List<Bus> output = new();
            try
            {
                output = Database.Query<Bus>("select BusId, Number from Bus Order by Number");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return output.ToList();
        }


        public async Task<List<BusSelect>> BusSelectList()
        {
            InitAsync();

            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            var output = await DatabaseAsync.QueryAsync<BusSelect>("select BusId as Key, Number as Value from Bus Order by BusId");
            return output.ToList();
            //}
        }

        public async Task<List<Busmessage>> MessageList(string BusId)
        {
            InitAsync();
            try
            {
                //using (var cnn = new SQLiteConnection(_dbPath))
                //{
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                //var output = cnn.Query<Busmessage>("select Id, BusId, Message, Sender, Priority, CreateDateTime, Status from Busmessage where BusId = @BusId and Status = 1 Order by Priority desc", parameters);
                var output = await DatabaseAsync.QueryAsync<Busmessage>("select Message, Sender, Priority from Busmessage where BusId = ? and Status = 1 Order by Priority desc", BusId);

                //var list = output.ToList();
                //string message = $"count = {list.Count}";
                //MessageBox.Show(message, "Message");
                return output.ToList();
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in MessageList: {ex.Message}");
            }
            return null;
        }

        public async Task<District> LoadDistrict()
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();
            var output = await DatabaseAsync.QueryAsync<District>("select Id, DistrictName, Address, City, State, Zip, Phone, CreateDate, ContactPhone1, ContactEmail1, Status from District");
            return output.FirstOrDefault();
            //}
        }

        public async Task<List<Route>> LoadRoutes(string BusId)
        {

            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
            var output = await DatabaseAsync.QueryAsync<Route>("select RouteNumber || ' ' || case when AmPm='0' then 'AM' else 'PM' End || ' ' || Description as Description, Id, RouteId, RouteNumber, BusId, Compound, Depot, AmPm from Route where Busid = ? order by AmPM, RouteNumber", BusId);
            return output.ToList();
            //}
        }

        //public StudentStop LoadStudentStop(string StudentNum, string StopId)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        DynamicParameters parameters = new DynamicParameters();

        //        parameters.Add("@StudentNum", StudentNum, DbType.String, ParameterDirection.Input);
        //        parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);

        //        var output = cnn.QuerySingle<StudentStop>("select s.StudentNumber, s.LastName, s.FirstName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Arrival, bs.Intersection1, bs.Intersection2, x.School from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join Busstop bs on ss.StopId = bs.StopId join School x on s.School = x.Id where s.StudentNumber = @StudentNumber and ss.StopId = @StopId", parameters);
        //        return output;
        //    }

        //    //SQLiteConnection con = new SQLiteConnection(@"data source=C:\Data\IVIS\ivis.db");
        //    //con.Open();
        //    //string query = "select s.StudentNumber, s.LastName, s.FirstName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Arrival, bs.Intersection1, bs.Intersection2, x.Name from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join Busstop bs on ss.StopId = bs.StopId join School x on s.School = x.Id where s.StudentNumber = @StudentNumber and ss.StopId = @StopId";
        //    //SQLiteCommand command = new SQLiteCommand(query, con);
        //    //command.Parameters.AddWithValue("@StudentNumber", StudentNum);
        //    //command.Parameters.AddWithValue("@StopId", StopId);
        //    //DataTable dt = new DataTable();
        //    //SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
        //    //var reader = command.ExecuteReader();
        //    //return reader;
        //}

        public async Task<Studentstopdetail> GetStopIdForStudentCardInRoute(string CardNumber, string RouteId, string AmPm)
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();

            //parameters.Add("@CardNumber", CardNumber, DbType.String, ParameterDirection.Input);
            //parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
            //parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);

            //var output = cnn.QuerySingle<Studentstopdetail>("select distinct ss.StudentNumber, ss.StopId " +
            //    "from StudentStop ss " +
            //    "join Busstop bs on ss.StopId = bs.StopId " +
            //    "join Student s on ss.StudentNumber = s.StudentNumber " +
            //    "where s.CardNumber = @CardNumber and ss.AmPm = @AmPm and bs.RouteId = @RouteId", parameters);

            var output = await DatabaseAsync.QueryAsync<Studentstopdetail>("select distinct s.Id, s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, null as Arrival, b.Number as BusNumber, bs.RouteId, ss.AmPm, bs.Intersection1, bs.Intersection2, x.Name as School, ss.StopId, ss.Status, ss.SeatNumber " +
                "from Student s " +
                "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                "join Busstop bs on ss.StopId = bs.StopId " +
                "join Bus b on bs.BusId = b.BusId " +
                "join School x on s.School = x.SchoolId " +
                "where s.CardNumber = ? and ss.AmPm = ? and bs.RouteId = ?", CardNumber, AmPm, RouteId);

            return output.FirstOrDefault();
            //}
        }

        public async Task<Busstop> GetBusStopByStopIdInRoute(string StopId, string RouteId, string AmPm)
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{

            var output = await DatabaseAsync.QueryAsync<Busstop>("select Id, RouteId, BusId, AmPm, StopId, Arrival, Intersection1, Intersection2 " +
                "from Busstop bs " +
                "where bs.StopId = ? and bs.AmPm = ? and bs.RouteId = ?", StopId, AmPm, RouteId);

            return output.FirstOrDefault();
            //}
        }

        public async Task<Studentstopdetail> LoadStudentStopDetail(string StudentNumber, string StopId, string AmPm)
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();

            //parameters.Add("@StudentNumber", StudentNumber, DbType.String, ParameterDirection.Input);
            //parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);

            var output = await DatabaseAsync.QueryAsync<Studentstopdetail>("select s.Id, s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Arrival, b.Number as BusNumber, bs.RouteId, ss.AmPm, bs.Intersection1, bs.Intersection2, x.Name as School, ss.StopId, ss.SeatNumber " +
                "from Student s " +
                "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                "join Busstop bs on ss.StopId = bs.StopId " +
                "join Bus b on bs.BusId = b.BusId " +
                "join School x on s.School = x.SchoolId " +
                "where s.StudentNumber = ? and ss.StopId = ? and ss.AmPm = ?", StudentNumber, StopId, AmPm);
            return output.FirstOrDefault();
            //}
        }

        //Possibly set this based on the time.
        public async Task<List<Busstop>> LoadGeoFenceList()
        //public List<Busstop> LoadGeoFenceList(string RouteId, string BusId, string AmPm)
        {
            InitAsync();
            string BusId = Global.BusId;
            string RouteId = Global.RouteId;
            string AmPm = Global.AmPm;
            if (RouteId == "" || AmPm == "")
            {
                return null;
            }
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
            //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
            //parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
            var output = await DatabaseAsync.QueryAsync<Busstop>("select StopId, Latitude, Longitude, Intersection1, Intersection2 from Busstop where RouteId = ? and BusId = ? and AmPm = ? order by Arrival", RouteId, BusId, AmPm);
            return output.ToList();
            //}
        }

        public async Task<List<Busstop>> LoadBusStops(string RouteId, string BusId, string AmPm)
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
            //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
            //parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
            var output = await DatabaseAsync.QueryAsync<Busstop>("select * from Busstop where RouteId = ? and BusId = ? and AmPm = ? order by Arrival", RouteId, BusId, AmPm);
            return output.ToList();
            //}
        }

        public async Task<List<Studentstopdetail>> LoadStudentStopDetails(string StopId, string AmPm)
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);
            //parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
            var output = await DatabaseAsync.QueryAsync<Studentstopdetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, b.Number as BusNumber, bs.RouteId, ss.AmPm, bs.Intersection1, bs.Intersection2, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  " +
                "from Student s " +
                "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                "join Busstop bs on ss.StopId = bs.StopId " +
                "join Bus b on bs.BusId = b.BusId " +
                "join School x on s.School = x.SchoolId " +
                "where ss.StopId = ? and ss.AmPm = ? order by LastName, FirstName", StopId, AmPm);
            //var output = cnn.Query<Studentstopdetail>("select s.Id, s.StudentNumber, s.FirstName, s.LastName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, ss.StopId, ss.SeatNumber, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join School x on s.School = x.Id where ss.StopId = @StopId order by FullName", parameters);
            return output.ToList();
            //}
        }

        public async Task<List<Studentstopdetail>> LoadStudentOnBusDetails()
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);
            var output = await DatabaseAsync.QueryAsync<Studentstopdetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, b.Number as BusNumber, bs.RouteId, ss.AmPm, bs.Intersection1, bs.Intersection2, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  " +
                "from Student s " +
                "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                "join Busstop bs on ss.StopId = bs.StopId " +
                "join Bus b on bs.BusId = b.BusId " +
                "join School x on s.School = x.SchoolId " +
                "where ss.Status = '1' order by LastName, FirstName");
            //var output = cnn.Query<Studentstopdetail>("select s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join School x on s.School = x.SchoolId where ss.StopId = @StopId order by Status, FullName", parameters);
            return output.ToList();
            //}
        }

        public async Task<List<Studentstopdetail>> LoadStudentBoardingBusDetails(string RouteId)
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
            var output = await DatabaseAsync.QueryAsync<Studentstopdetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join Busstop bs on ss.StopId = bs.StopId join School x on s.School = x.SchoolId where bs.RouteId = ? order by LastName, FirstName", RouteId);
            return output.ToList();
            //}
        }

        //public List<StatusQueue> GetUploadStatusQueue(StatusQueue queue)
        public async Task<List<StatusQueue>> GetUploadStatusQueue()
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            var output = await DatabaseAsync.QueryAsync<StatusQueue>("select BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, StatusDate from StatusQueue " +
            "where Uploaded = 0");// and BusNumber = ? and routeId = ? and AmPm = ? and " +
                                  //"StopId = ? and StudentNumber = ? and Status = ? and StatusDate = ?", queue.BusNumber, queue.RouteId, queue.AmPm, queue.StopId, queue.Status, queue.StatusDate);

            return output.ToList();
            //}
        }

        public async Task<List<StatusQueue>> LoadStatusQueue()
        {
            InitAsync();
            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            //DynamicParameters parameters = new DynamicParameters();
            var output = await DatabaseAsync.QueryAsync<StatusQueue>("select BusNumber, StopId, StudentNumber, Status, StatusDate from StatusQueue");
            return output.ToList();
            //}
        }

        //public string insertStatusQueue(StatusQueue statusqueue)
        //{
        //    DataService dataservice = new Data.DataService();
        //    //DynamicParameters param = new DynamicParameters();
        //    //param.Add("@DistrictId", statusqueue.DistrictId);
        //    //param.Add("@BusNumber", statusqueue.BusNumber);
        //    //param.Add("@RouteId", statusqueue.RouteId);
        //    //param.Add("@AmPm", statusqueue.AmPm);
        //    //param.Add("@StopId", statusqueue.StopId);
        //    //param.Add("@StudentNumber", statusqueue.StudentNumber);
        //    //param.Add("@Status", statusqueue.Status);
        //    //param.Add("@StatusDate", statusqueue.StatusDateDate);
        //    return dataservice.executeNonQuery("Insert_StatusQueue", param);
        //}


    //public async void insertStatus(StatusQueue statusqueue)
    //{
    //    string url = "";
    //    HttpClient.client = new HttpClient();
    //    HttpRequestMessage message = new HttpMessage(HttpMethod.Post, url);
    //    message.Content = JsonContent.Create<StatusQueue>(statusqueue);
    //    HttpResponseMessage response = await client.SendAsync(message);
    //}

    public async Task SaveStatus(string BusNumber, string RouteId, string StopId, string StudentNumber, int Status, string AmPm)
        //public void SaveStatus(Studentstopdetail ssd)
        {
            InitAsync();
            string StatusDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
            try
            {
                //await DatabaseAsync.ExecuteAsync($"Insert into StatusQueue(BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, StatusDate, Uploaded) " +
                //    $"values ('{BusNumber}', '{RouteId}', '{AmPm}', '{StopId}', '{StudentNumber}', {Status}, '{StatusDate}', 0);");
                StatusQueue statusqueue = new StatusQueue()
                {
                    BusNumber = BusNumber,
                    RouteId = RouteId,
                    AmPm = AmPm,
                    StopId = StopId,
                    StudentNumber = StudentNumber,
                    Status = Status,
                    StatusDate = StatusDate,
                    Uploaded = 0
                };
                var rc = await DatabaseAsync.InsertAsync(statusqueue);
                if (rc == 1)
                {
                    //Todo:  If successful update uploaded flag.

                    Console.WriteLine("Success");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //cnn.Execute($"Insert into StatusQueue(BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, StatusDate, Uploaded) " +
            //    $"values ({BusNumber}, {RouteId}, {AmPm}, {StopId}, {StudentNumber}, {Status}, {StatusDate}, 0)");

            //}

            var district = LoadDistrict();
            //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultSql"].ConnectionString);
            //            string qry;
            //            con.Open();

            try
            {

                //Check if we have db connection
                //Get list of records not uploaded from sqlite
                //Loop through list
                //Upload item to sql server
                //Mark item uploaded in sqlite
                //var statusList = GetUploadStatusQueue(statusqueue);
                var statusList = await GetUploadStatusQueue();
                foreach (var status in statusList)
                {
                    status.DistrictId = district.Id;
                    //status.StatusDateDate = DateTime.Parse(status.StatusDate);
                    //Insert into sql server.
                    //Temporary
                    //insertStatusQueue(status);
                    var resultSuccess = await _statusqueueService.SendTaskAsync(status);

                    if (resultSuccess)
                    {
                        string updateStatus = @"Update StatusQueue Set Uploaded = 1 Where BusNumber = '@BusNumber' and routeId = '@RouteId' and AmPm = '@AmPm' and StopId = '@StopId' and StudentNumber = '@StudentNumber' and Status = @Status and StatusDate = '@StatusDate';";

                        //using (var cnn = new SQLiteConnection(_dbPath))
                        //{
                        var result = await DatabaseAsync.ExecuteAsync(updateStatus, new
                        {
                            BusNumber,
                            RouteId,
                            AmPm,
                            StopId,
                            StudentNumber,
                            Status,
                            StatusDate
                        });
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message, "Message");
            }

            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
                await DatabaseAsync.ExecuteAsync($"Update StudentStop Set Status = {Status} Where StudentNumber = {StudentNumber} and StopId = {StopId} and AmPm = {AmPm}");
            //}
        }

        //public void UnloadBus(string BusId, string StopId)
        public async Task UnloadBus(string BusNumber)
        {
            InitAsync();
            string Now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
                //var output = cnn.Query<StatusQueue>("Insert into StatusQueue(BusNumber, StopId, StudentNumber, Status, StatusDate) Select b.Number, bs.StopId, ss.StudentNumber, 0 as Status, ? as StatusDate From Bus b join Route r on b.BusId = r.BusId join Busstop bs on r.RouteId = bs.RouteId join StudentStop ss on bs.StopId = ss.StopId where b.Number = ? and ss.Status = 1", Now, BusNumber);

                await DatabaseAsync.ExecuteAsync($"Insert into StatusQueue(BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, StatusDate, Uploaded) " +
                    $"Select b.Number, r.RouteId, r.AmPm, bs.StopId, ss.StudentNumber, 0 as Status, {Now} as StatusDate, 0 as Uploaded " +
                    $"From Bus b join Route r on b.BusId = r.BusId " +
                    $"join Busstop bs on r.RouteId = bs.RouteId " +
                    $"join StudentStop ss on bs.StopId = ss.StopId " +
                    $"where b.Number = {BusNumber} and ss.Status = 1");

            //}

            //using (SQLiteConnection sqLiteConnection = new SQLiteConnection(LoadConnectionString()))
            //{
            //    sqLiteConnection.Open();

            //    SQLiteCommand updateCmd = new SQLiteCommand(sqLiteConnection)
            //    {
            //        CommandText = "Update StudentStop Set Status = 0 Where Status = 1"
            //    };

            //    updateCmd.ExecuteNonQuery();

            //    sqLiteConnection.Close();
            //}


            //using (var cnn = new SQLiteConnection(_dbPath))
            //{
                //string updateQuery = @"Update StudentStop Set Status = 0 where Status = 1";
                //string updateQuery = @"Update StudentStop Set StudentStop.Status = 0 From StudentStop join Busstop bs on StudentStop.StopId = bs.StopId join Route r on bs.RouteId = r.RouteId join Bus b on r.BusId = b.BusId where b.Number = @BusNumber and StudentStop.Status = 1";
                //cnn.Execute("Update StudentStop Set Status = @Status Where StudentNumber = @StudentNumber and StopId = @StopId", Status, StudentNumber, StopId);
                //var result = cnn.Execute(updateQuery);

                await DatabaseAsync.ExecuteAsync($"Update StudentStop Set Status = 0 where Status = 1");
            //}
        }
    }
}
