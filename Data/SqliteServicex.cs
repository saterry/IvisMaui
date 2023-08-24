//using Dapper;
//using IvisMaui.Model;
//using Android.Media;
using IvisMaui.Model;
using SQLite;
using System.Data;
using System.Diagnostics;
//using static Android.Icu.Text.CaseMap;
//using static Android.InputMethodServices.Keyboard;
//using System;
//using System.Data;
//using Android.Hardware.Camera;

namespace IvisMaui.Data
{
    public class SqliteServicex
    {

        string _dbPath;
        private SQLiteConnection conn;

        public SqliteService(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void CreateStudentTable()
        {
            try
            {
                conn = new SQLiteConnection(_dbPath);
                conn.CreateTable<Student>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddStudent(Student student)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Insert(student);
        }

        public List<Student> GetAllStudents()
        {
            try
            {
                CreateStudentTable();
                return conn.Table<Student>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<Student>();
        }

        public List<Student> LoadStudents(string BusId)
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input, BusId.Length);
                //parameters.Add("BusId", BusId);
                //var output = cnn.Query<Student>("select s.StudentNumber, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, x.Name as School from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join Busstop bs on ss.StopId = bs.StopId join School x on s.School = x.Id where bs.BusId = @BusId order by FullName", parameters);
                //var output = cnn.Query<Student>("select distinct s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, x.Name as School from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join Busstop bs on ss.StopId = bs.StopId join School x on s.School = x.SchoolId where bs.BusId = @BusId order by LastName, FirstName", parameters);
                var output = cnn.Query<Student>("select distinct s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Intersection1, bs.Intersection2, x.Name as School " +
                    "from Student s " +
                    "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                    "join Busstop bs on ss.StopId = bs.StopId " +
                    "join School x on s.School = x.SchoolId " +
                    "where bs.BusId = ? " +
                    "order by LastName, FirstName", BusId);
                return output.ToList();
            }
        }

        public Student LoadStudent(string StudentNumber)
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {

                //parameters.Add("@StudentNumber", StudentNumber, DbType.String, ParameterDirection.Input);

                var output = cnn.Query<Student>("select s.Id as Id, s.StudentNumber as StudentNumber, s.LastName as LastName, s.FirstName as FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone as Phone, s.Grade as Grade, s.DOB as DOB, s.Address as Address, s.City as City, s.State as State, s.Zip as Zip, bs.Intersection1, bs.Intersection2, x.Name as School " +
                    "from Student s " +
                    "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                    "join Busstop bs on ss.StopId = bs.StopId " +
                    "join School x on s.School = x.Id " +
                    "where s.StudentNumber = ?", StudentNumber).FirstOrDefault();
                return output;
            }
        }

        public void DeleteStudent(int id)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Delete(new Student { Id = id });
        }

        public List<Bus> BusList()
        {

            using (var cnn = new SQLiteConnection(_dbPath))
            {
                var output = cnn.Query<Bus>("select BusId, Number from Bus Order by Number");
                return output.ToList();
            }
        }

        public List<BusSelect> BusSelectList()
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                var output = cnn.Query<BusSelect>("select BusId as Key, Number as Value from Bus Order by BusId");
                return output.ToList();
            }
        }

        public List<Busmessage> MessageList(string BusId)
        {
            try
            {
                using (var cnn = new SQLiteConnection(_dbPath))
                {
                    //DynamicParameters parameters = new DynamicParameters();
                    //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                    //var output = cnn.Query<Busmessage>("select Id, BusId, Message, Sender, Priority, CreateDateTime, Status from Busmessage where BusId = @BusId and Status = 1 Order by Priority desc", parameters);
                    var output = cnn.Query<Busmessage>("select Message, Sender, Priority from Busmessage where BusId = ? and Status = 1 Order by Priority desc", BusId);

                    var list = output.ToList();
                    //string message = $"count = {list.Count}";
                    //MessageBox.Show(message, "Message");
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in MessageList: {ex.Message}");
            }
            return null;
        }

        public District LoadDistrict()
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                var output = cnn.Query<District>("select Id, DistrictName, Address, City, State, Zip, Phone, CreateDate, ContactPhone1, ContactEmail1, Status from District").FirstOrDefault();
                return output;
            }
        }

        public List<Route> LoadRoutes(string BusId)
        {

            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<Route>("select RouteNumber || ' ' || case when AmPm='0' then 'AM' else 'PM' End as Description, Id, RouteId, RouteNumber, BusId, Compound, Depot, AmPm from Route where Busid = ? order by AmPM, RouteNumber", BusId);
                return output.ToList();
            }
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

        public Studentstopdetail GetStopIdForStudentCardInRoute(string CardNumber, string RouteId, string AmPm)
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();

                //parameters.Add("@CardNumber", CardNumber, DbType.String, ParameterDirection.Input);
                //parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
                //parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);

                //var output = cnn.QuerySingle<Studentstopdetail>("select distinct ss.StudentNumber, ss.StopId " +
                //    "from StudentStop ss " +
                //    "join Busstop bs on ss.StopId = bs.StopId " +
                //    "join Student s on ss.StudentNumber = s.StudentNumber " +
                //    "where s.CardNumber = @CardNumber and ss.AmPm = @AmPm and bs.RouteId = @RouteId", parameters);

                var output = cnn.Query<Studentstopdetail>("select distinct s.Id, s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, null as Arrival, b.Number as BusNumber, bs.RouteId, ss.AmPm, bs.Intersection1, bs.Intersection2, x.Name as School, ss.StopId, ss.Status, ss.SeatNumber " +
                    "from Student s " +
                    "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                    "join Busstop bs on ss.StopId = bs.StopId " +
                    "join Bus b on bs.BusId = b.BusId " +
                    "join School x on s.School = x.SchoolId " +
                    "where s.CardNumber = ? and ss.AmPm = ? and bs.RouteId = ?", CardNumber, AmPm, RouteId).FirstOrDefault();

                return output;
            }
        }

        public Busstop GetBusStopByStopIdInRoute(string StopId, string RouteId, string AmPm)
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {

                var output = cnn.Query<Busstop>("select Id, RouteId, BusId, AmPm, StopId, Arrival, Intersection1, Intersection2 " +
                    "from Busstop bs " +
                    "where bs.StopId = ? and bs.AmPm = ? and bs.RouteId = ?", StopId, AmPm, RouteId).FirstOrDefault();

                return output;
            }
        }

        public Studentstopdetail LoadStudentStopDetail(string StudentNumber, string StopId, string AmPm)
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();

                //parameters.Add("@StudentNumber", StudentNumber, DbType.String, ParameterDirection.Input);
                //parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);

                var output = cnn.Query<Studentstopdetail>("select s.Id, s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Arrival, b.Number as BusNumber, bs.RouteId, ss.AmPm, bs.Intersection1, bs.Intersection2, x.Name as School, ss.StopId, ss.SeatNumber " +
                    "from Student s " +
                    "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                    "join Busstop bs on ss.StopId = bs.StopId " +
                    "join Bus b on bs.BusId = b.BusId " +
                    "join School x on s.School = x.SchoolId " +
                    "where s.StudentNumber = ? and ss.StopId = ? and ss.AmPm = ?", StudentNumber, StopId, AmPm).FirstOrDefault();
                return output;
            }
        }

        //Possibly set this based on the time.
        public List<Busstop> LoadGeoFenceList()
        //public List<Busstop> LoadGeoFenceList(string RouteId, string BusId, string AmPm)
        {
            string BusId = Global.BusId;
            string RouteId = Global.RouteId;
            string AmPm = Global.AmPm;
            if (RouteId == "" || AmPm == "")
            {
                return null;
            }
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
                //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                //parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<Busstop>("select StopId, Latitude, Longitude, Intersection1, Intersection2 from Busstop where RouteId = ? and BusId = ? and AmPm = ? order by Arrival", RouteId, BusId, AmPm);
                return output.ToList();
            }
        }

        public List<Busstop> LoadBusStops(string RouteId, string BusId, string AmPm)
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
                //parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                //parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<Busstop>("select * from Busstop where RouteId = ? and BusId = ? and AmPm = ? order by Arrival", RouteId, BusId, AmPm);
                return output.ToList();
            }
        }

        public List<Studentstopdetail> LoadStudentStopDetails(string StopId, string AmPm)
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);
                //parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<Studentstopdetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, b.Number as BusNumber, bs.RouteId, ss.AmPm, bs.Intersection1, bs.Intersection2, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  " +
                    "from Student s " +
                    "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                    "join Busstop bs on ss.StopId = bs.StopId " +
                    "join Bus b on bs.BusId = b.BusId " +
                    "join School x on s.School = x.SchoolId " +
                    "where ss.StopId = ? and ss.AmPm = ? order by LastName, FirstName", StopId, AmPm);
                //var output = cnn.Query<Studentstopdetail>("select s.Id, s.StudentNumber, s.FirstName, s.LastName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, ss.StopId, ss.SeatNumber, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join School x on s.School = x.Id where ss.StopId = @StopId order by FullName", parameters);
                return output.ToList();
            }
        }

        public List<Studentstopdetail> LoadStudentOnBusDetails()
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<Studentstopdetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, b.Number as BusNumber, bs.RouteId, ss.AmPm, bs.Intersection1, bs.Intersection2, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  " +
                    "from Student s " +
                    "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                    "join Busstop bs on ss.StopId = bs.StopId " +
                    "join Bus b on bs.BusId = b.BusId " +
                    "join School x on s.School = x.SchoolId " +
                    "where ss.Status = '1' order by LastName, FirstName");
                //var output = cnn.Query<Studentstopdetail>("select s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join School x on s.School = x.SchoolId where ss.StopId = @StopId order by Status, FullName", parameters);
                return output.ToList();
            }
        }

        public List<Studentstopdetail> LoadStudentBoardingBusDetails(string RouteId)
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<Studentstopdetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join Busstop bs on ss.StopId = bs.StopId join School x on s.School = x.SchoolId where bs.RouteId = ? order by LastName, FirstName", RouteId);
                return output.ToList();
            }
        }

        //public List<StatusQueue> GetUploadStatusQueue(StatusQueue queue)
        public List<StatusQueue> GetUploadStatusQueue()
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                var output = cnn.Query<StatusQueue>("select BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, StatusDate from StatusQueue " +
                "where Uploaded = 0");// and BusNumber = ? and routeId = ? and AmPm = ? and " +
                //"StopId = ? and StudentNumber = ? and Status = ? and StatusDate = ?", queue.BusNumber, queue.RouteId, queue.AmPm, queue.StopId, queue.Status, queue.StatusDate);

                return output.ToList();
            }
        }

        public List<StatusQueue> LoadStatusQueue()
        {
            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //DynamicParameters parameters = new DynamicParameters();
                var output = cnn.Query<StatusQueue>("select BusNumber, StopId, StudentNumber, Status, StatusDate from StatusQueue");
                return output.ToList();
            }
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

        public void SaveStatus(string BusNumber, string RouteId, string StopId, string StudentNumber, int Status, string AmPm)
        //public void SaveStatus(Studentstopdetail ssd)
        {
            string StatusDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            using (var cnn = new SQLiteConnection(_dbPath))
            {
                cnn.Execute($"Insert into StatusQueue(BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, Uploaded) " +
                    $"values ({BusNumber}, {RouteId}, {AmPm}, {StopId}, {StudentNumber}, {Status}, 0)");

                //cnn.Execute($"Insert into StatusQueue(BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, StatusDate, Uploaded) " +
                //    $"values ({BusNumber}, {RouteId}, {AmPm}, {StopId}, {StudentNumber}, {Status}, {StatusDate}, 0)");

            }

            //TODO Get district, Insert into sql
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
                var statusList = GetUploadStatusQueue();
                foreach (var status in statusList)
                {
                    status.DistrictId = district.Id;
                    status.StatusDateDate = DateTime.Parse(status.StatusDate);
                    //Insert into sql server.
                    //Temporary
                    //insertStatusQueue(status);

                    string updateStatus = @"Update StatusQueue Set Uploaded = 1 Where BusNumber = @BusNumber and routeId = @RouteId and AmPm = @AmPm and StopId = @StopId and StudentNumber = @StudentNumber and Status = @Status and StatusDate = @StatusDate";

                    using (var cnn = new SQLiteConnection(_dbPath))
                    {
                        var result = cnn.Execute(updateStatus, new
                        {
                            BusNumber,
                            RouteId,
                            AmPm,
                            StopId,
                            StudentNumber,
                            Status,
                            StatusDate
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Message");
            }

            using (var cnn = new SQLiteConnection(_dbPath))
            {
                cnn.Execute($"Update StudentStop Set Status = {Status} Where StudentNumber = {StudentNumber} and StopId = {StopId} and AmPm = {AmPm}");
            }
        }

        //public void UnloadBus(string BusId, string StopId)
        public void UnloadBus(string BusNumber)
        {
            string Now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //var output = cnn.Query<StatusQueue>("Insert into StatusQueue(BusNumber, StopId, StudentNumber, Status, StatusDate) Select b.Number, bs.StopId, ss.StudentNumber, 0 as Status, ? as StatusDate From Bus b join Route r on b.BusId = r.BusId join Busstop bs on r.RouteId = bs.RouteId join StudentStop ss on bs.StopId = ss.StopId where b.Number = ? and ss.Status = 1", Now, BusNumber);

                cnn.Execute($"Insert into StatusQueue(BusNumber, StopId, StudentNumber, Status, StatusDate) " +
                    $"Select b.Number, bs.StopId, ss.StudentNumber, 0 as Status, {Now} as StatusDate From Bus b join Route r on b.BusId = r.BusId " +
                    $"join Busstop bs on r.RouteId = bs.RouteId join StudentStop ss on bs.StopId = ss.StopId " +
                    $"where b.Number = {BusNumber} and ss.Status = 1");

            }

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


            using (var cnn = new SQLiteConnection(_dbPath))
            {
                //string updateQuery = @"Update StudentStop Set Status = 0 where Status = 1";
                //string updateQuery = @"Update StudentStop Set StudentStop.Status = 0 From StudentStop join Busstop bs on StudentStop.StopId = bs.StopId join Route r on bs.RouteId = r.RouteId join Bus b on r.BusId = b.BusId where b.Number = @BusNumber and StudentStop.Status = 1";
                //cnn.Execute("Update StudentStop Set Status = @Status Where StudentNumber = @StudentNumber and StopId = @StopId", Status, StudentNumber, StopId);
                //var result = cnn.Execute(updateQuery);

                cnn.Execute($"Update StudentStop Set Status = 0 where Status = 1");


            }
        }
    }
}
