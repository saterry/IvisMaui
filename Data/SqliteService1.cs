using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using System.Data.SQLite;
using System.Configuration;
//using Dapper;
using IvisMaui.Model;
using IvisMaui.Data;
using Microsoft.Extensions.Configuration;
//using System.Windows.Forms;

namespace IvisMAUI.Data
{
    class SqliteService1
    {

        public static List<Bus> BusList()
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Bus>("select BusId, Number from Bus Order by BusId", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<BusSelect> BusSelectList()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<BusSelect>("select BusId as Key, Number as Value from Bus Order by BusId", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<BusMessage> MessageList(string BusId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                    //var output = cnn.Query<BusMessage>("select Id, BusId, Message, Sender, Priority, CreateDateTime, Status from BusMessage where BusId = @BusId and Status = 1 Order by Priority desc", parameters);
                    var output = cnn.Query<BusMessage>("select Message, Sender, Priority from BusMessage where BusId = @BusId and Status = 1 Order by Priority desc", parameters);

                    var list = output.ToList();
                    //string message = $"count = {list.Count}";
                    //MessageBox.Show(message, "Message");
                    return  list;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Message");
            }
            return null;
        }

        public static District LoadDistrict()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                var output = cnn.QuerySingle<District>("select Id, DistrictName, Address, City, State, Zip, Phone, CreateDate, ContactPhone1, ContactEmail1, Status from District");
                return output;
            }
        }

        public static List<Student1> LoadStudents(string BusId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                //var output = cnn.Query<Student>("select s.StudentNumber, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, x.Name as School from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join BusStop bs on ss.StopId = bs.StopId join School x on s.School = x.Id where bs.BusId = @BusId order by FullName", parameters);
                var output = cnn.Query<Student1>("select distinct s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, x.Name as School from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join BusStop bs on ss.StopId = bs.StopId join School x on s.School = x.SchoolId where bs.BusId = @BusId order by LastName, FirstName", parameters);
                return output.ToList();
            }
        }

        public static List<Route> LoadRoutes(string BusId)
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<Route>("select RouteNumber || ' ' || case when AmPm='0' then 'AM' else 'PM' End as Description, Id, RouteId, RouteNumber, BusId, Compound, Depot, AmPm from Route where Busid = @BusId order by AmPM, RouteNumber", parameters);
                return output.ToList();
            }
        }

        //public static StudentStop LoadStudentStop(string StudentNum, string StopId)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //    {
        //        DynamicParameters parameters = new DynamicParameters();

        //        parameters.Add("@StudentNum", StudentNum, DbType.String, ParameterDirection.Input);
        //        parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);

        //        var output = cnn.QuerySingle<StudentStop>("select s.StudentNumber, s.LastName, s.FirstName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Arrival, bs.Intersection1, bs.Intersection2, x.School from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join BusStop bs on ss.StopId = bs.StopId join School x on s.School = x.Id where s.StudentNumber = @StudentNumber and ss.StopId = @StopId", parameters);
        //        return output;
        //    }

        //    //SQLiteConnection con = new SQLiteConnection(@"data source=C:\Data\IVIS\ivis.db");
        //    //con.Open();
        //    //string query = "select s.StudentNumber, s.LastName, s.FirstName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Arrival, bs.Intersection1, bs.Intersection2, x.Name from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join BusStop bs on ss.StopId = bs.StopId join School x on s.School = x.Id where s.StudentNumber = @StudentNumber and ss.StopId = @StopId";
        //    //SQLiteCommand command = new SQLiteCommand(query, con);
        //    //command.Parameters.AddWithValue("@StudentNumber", StudentNum);
        //    //command.Parameters.AddWithValue("@StopId", StopId);
        //    //DataTable dt = new DataTable();
        //    //SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
        //    //var reader = command.ExecuteReader();
        //    //return reader;
        //}

        public static StudentStopDetail GetStopIdForStudentCardInRoute(string CardNumber, string RouteId, string AmPm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@CardNumber", CardNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
                parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);

                //var output = cnn.QuerySingle<StudentStopDetail>("select distinct ss.StudentNumber, ss.StopId " +
                //    "from StudentStop ss " +
                //    "join BusStop bs on ss.StopId = bs.StopId " +
                //    "join Student s on ss.StudentNumber = s.StudentNumber " +
                //    "where s.CardNumber = @CardNumber and ss.AmPm = @AmPm and bs.RouteId = @RouteId", parameters);

                var output = cnn.QuerySingleOrDefault<StudentStopDetail>("select distinct s.Id, s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, null as Arrival, bs.Intersection1, bs.Intersection2, x.Name as School, ss.StopId, ss.Status, ss.SeatNumber " +
                    "from Student s " +
                    "join StudentStop ss on s.StudentNumber = ss.StudentNumber " +
                    "join BusStop bs on ss.StopId = bs.StopId " +
                    "join School x on s.School = x.SchoolId " +
                    "where s.CardNumber = @CardNumber and ss.AmPm = @AmPm and bs.RouteId = @RouteId", parameters);

                return output;
            }
        }

        public static StudentStopDetail LoadStudentStopDetail(string StudentNumber, string StopId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@StudentNumber", StudentNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);

                var output = cnn.QuerySingle<StudentStopDetail>("select s.Id, s.StudentNumber, s.LastName, s.FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone, s.Grade, s.DOB, s.Address, s.City, s.State, s.Zip, bs.Arrival, bs.Intersection1, bs.Intersection2, x.Name as School, ss.StopId, ss.SeatNumber from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join BusStop bs on ss.StopId = bs.StopId join School x on s.School = x.SchoolId where s.StudentNumber = @StudentNumber and ss.StopId = @StopId", parameters);
                return output;
            }
        }

        public static Student1 LoadStudent(string StudentNumber)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@StudentNumber", StudentNumber, DbType.String, ParameterDirection.Input);

                var output = cnn.QuerySingle<Student1>("select s.Id as Id, s.StudentNumber as StudentNumber, s.LastName as LastName, s.FirstName as FirstName, s.LastName || ', ' || s.FirstName as FullName, s.Phone as Phone, s.Grade as Grade, s.DOB as DOB, s.Address as Address, s.City as City, s.State as State, s.Zip as Zip, x.Name as School from Student s join School x on s.School = x.Id where s.StudentNumber = @StudentNumber", parameters);
                return output;
            }
        }

        //Possibly set this based on the time.
        public static List<BusStop> LoadGeoFenceList()
        //public static List<BusStop> LoadGeoFenceList(string RouteId, string BusId, string AmPm)
        {
            string BusId = Global.BusId;
            string RouteId = Global.RouteId;
            string AmPm = Global.AmPm;
            if (RouteId == "" || AmPm == "")
            {
                return null;
            }
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
                parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<BusStop>("select StopId, Latitude, Longitude, Intersection1, Intersection2 from BusStop where RouteId = @RouteId and BusId = @BusId and AmPm = @AmPm order by Arrival", parameters);
                return output.ToList();
            }
        }

        public static List<BusStop> LoadBusStops(string RouteId, string BusId, string AmPm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
                parameters.Add("@BusId", BusId, DbType.String, ParameterDirection.Input);
                parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<BusStop>("select * from BusStop where RouteId = @RouteId and BusId = @BusId and AmPm = @AmPm order by Arrival", parameters);
                return output.ToList();
            }
        }

        public static List<StudentStopDetail> LoadStudentStopDetails(string StopId, string AmPm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);
                parameters.Add("@AmPm", AmPm, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<StudentStopDetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join School x on s.School = x.SchoolId where ss.StopId = @StopId and ss.AmPm = @AmPm order by LastName, FirstName", parameters);
                //var output = cnn.Query<StudentStopDetail>("select s.Id, s.StudentNumber, s.FirstName, s.LastName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, ss.StopId, ss.SeatNumber, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join School x on s.School = x.Id where ss.StopId = @StopId order by FullName", parameters);
                return output.ToList();
            }
        }

        public static List<StudentStopDetail> LoadStudentOnBusDetails()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@StopId", StopId, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<StudentStopDetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join School x on s.School = x.SchoolId where ss.Status = '1' order by LastName, FirstName", parameters);
                //var output = cnn.Query<StudentStopDetail>("select s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join School x on s.School = x.SchoolId where ss.StopId = @StopId order by Status, FullName", parameters);
                return output.ToList();
            }
        }

        public static List<StudentStopDetail> LoadStudentBoardingBusDetails(string RouteId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RouteId", RouteId, DbType.String, ParameterDirection.Input);
                var output = cnn.Query<StudentStopDetail>("select distinct s.Id, s.StudentNumber, s.FirstName, s.LastName, s.LastName || ', ' || s.FirstName as FullName, s.Address, s.City, s.State, s.Zip, s.Phone, s.Grade, s.DOB, ss.StopId, ss.SeatNumber, ss.Status, x.Name as School  from Student s join StudentStop ss on s.StudentNumber = ss.StudentNumber join BusStop bs on ss.StopId = bs.StopId join School x on s.School = x.SchoolId where bs.RouteId = @RouteId order by LastName, FirstName", parameters);
                return output.ToList();
            }
        }

        public static List<StatusQueue> GetUploadStatusQueue(StatusQueue queue)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@BusNumber", queue.BusNumber, DbType.String, ParameterDirection.Input);
                //parameters.Add("@RouteId", queue.RouteId);
                //parameters.Add("@AmPm", queue.AmPm);
                //parameters.Add("@StopId", queue.StopId);
                //parameters.Add("@StudentNumber", queue.StudentNumber);
                //parameters.Add("@Status", queue.Status);
                //parameters.Add("@StatusDate", queue.StatusDate);
                var output = cnn.Query<StatusQueue>("select BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, StatusDate from StatusQueue " +
                                                    "where Uploaded = 0");
                //"where Uploaded = 0 and BusNumber = @BusNumber and routeId = @RouteId and AmPm = @AmPm and " +
                //"StopId = @StopId and StudentNumber = @StudentNumber and Status = @Status and StatusDate = @StatusDate", parameters);


                return output.ToList();
            }
        }

        public static List<StatusQueue> LoadStatusQueue()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                var output = cnn.Query<StatusQueue>("select BusNumber, StopId, StudentNumber, Status, StatusDate from StatusQueue");
                return output.ToList();
            }
        }

        //public static string insertStatusQueue(StatusQueue statusqueue)
        //{
        //    DataService dataservice = new Data.DataService();
        //    DynamicParameters param = new DynamicParameters();
        //    param.Add("@DistrictId", statusqueue.DistrictId);
        //    param.Add("@BusNumber", statusqueue.BusNumber);
        //    param.Add("@RouteId", statusqueue.RouteId);
        //    param.Add("@AmPm", statusqueue.AmPm);
        //    param.Add("@StopId", statusqueue.StopId);
        //    param.Add("@StudentNumber", statusqueue.StudentNumber);
        //    param.Add("@Status", statusqueue.Status);
        //    param.Add("@StatusDate", statusqueue.StatusDateDate);
        //    return dataservice.executeNonQuery("Insert_StatusQueue", param);
        //}


        public static void SaveStatus(StatusQueue statusqueue, string BusNumber, string StopId, string StudentNumber, int Status, string AmPm)
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into StatusQueue(BusNumber, RouteId, AmPm, StopId, StudentNumber, Status, StatusDate, Uploaded) values (@BusNumber, @RouteId, @AmPm, @StopId, @StudentNumber, @Status, @StatusDate, 0)", statusqueue);
                //cnn.commit();
            }

            //TODO Get district, Insert into sql
            var district = SqliteService1.LoadDistrict();
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
                var statusList = GetUploadStatusQueue(statusqueue);
                foreach (var status in statusList)
                {
                    status.DistrictId = district.Id;
                    status.StatusDateDate = DateTime.Parse(status.StatusDate);
                    //Insert into sql server.
                    //Temporary
                    //insertStatusQueue(status);

                    string updateStatus = @"Update StatusQueue Set Uploaded = 1 Where BusNumber = @BusNumber and routeId = @RouteId and AmPm = @AmPm and StopId = @StopId and StudentNumber = @StudentNumber and Status = @Status and StatusDate = @StatusDate";

                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        var result = cnn.Execute(updateStatus, new
                        {
                            statusqueue.BusNumber,
                            statusqueue.RouteId,
                            statusqueue.AmPm,
                            statusqueue.StopId,
                            statusqueue.StudentNumber,
                            statusqueue.Status,
                            statusqueue.StatusDate
                        });
                    }
                }
            }
            catch (Exception )
            {
                //MessageBox.Show(ex.Message, "Message");
            }
            //            con.Close();

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string updateQuery = @"Update StudentStop Set Status = @Status Where StudentNumber = @StudentNumber and StopId = @StopId and AmPm = @AmPm";
                //cnn.Execute("Update StudentStop Set Status = @Status Where StudentNumber = @StudentNumber and StopId = @StopId", Status, StudentNumber, StopId);
                var result = cnn.Execute(updateQuery, new
                {
                    Status,
                    StudentNumber,
                    StopId,
                    AmPm
                });
            }
        }

        //public static void UnloadBus(string BusId, string StopId)
        public static void UnloadBus(string BusNumber)
        {
            string Now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BusNumber", BusNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("@Now", Now, DbType.String, ParameterDirection.Input);
                //var output = cnn.Query<BusMessage>("select Id, BusId, Message, Sender, Priority, CreateDateTime, Status from BusMessage where BusId = @BusId and Status = 1 Order by Priority desc", parameters);
                var output = cnn.Query<StatusQueue>("Insert into StatusQueue(BusNumber, StopId, StudentNumber, Status, StatusDate) Select b.Number, bs.StopId, ss.StudentNumber, 0 as Status, @Now as StatusDate From Bus b join Route r on b.BusId = r.BusId join BusStop bs on r.RouteId = bs.RouteId join StudentStop ss on bs.StopId = ss.StopId where b.Number = @BusNumber and ss.Status = 1", parameters);
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


            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string updateQuery = @"Update StudentStop Set Status = 0 where Status = 1";
                //string updateQuery = @"Update StudentStop Set StudentStop.Status = 0 From StudentStop join BusStop bs on StudentStop.StopId = bs.StopId join Route r on bs.RouteId = r.RouteId join Bus b on r.BusId = b.BusId where b.Number = @BusNumber and StudentStop.Status = 1";
                //cnn.Execute("Update StudentStop Set Status = @Status Where StudentNumber = @StudentNumber and StopId = @StopId", Status, StudentNumber, StopId);
                var result = cnn.Execute(updateQuery);
            }
        }

        public static string LoadConnectionString(string id = "Default")
        {
            //return Global.ConnectionString;
            //return ConfigurationManager.ConnectionStrings[id].ConnectionString;
            return null;
        }

        //public static DataTable ClassToDataTable<T>(List<T> items)
        //{
        //    DataTable dataTable = new DataTable(typeof(T).Name);

        //    //Get all the properties  
        //    PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (PropertyInfo prop in Props)
        //    {
        //        //Defining type of data column gives proper data table   
        //        var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
        //        //Setting column names as Property names  
        //        dataTable.Columns.Add(prop.Name, type);
        //    }
        //    foreach (T item in items)
        //    {
        //        var values = new object[Props.Length];
        //        for (int i = 0; i < Props.Length; i++)
        //        {
        //            //inserting property values to datatable rows  
        //            values[i] = Props[i].GetValue(item, null);
        //        }
        //        dataTable.Rows.Add(values);
        //    }
        //    //put a breakpoint here and check datatable  
        //    return dataTable;
        //}
    }
}
