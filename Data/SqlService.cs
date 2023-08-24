using System;
using System.Data;
using System.Data.SqlClient;
//using Dapper;
using System.Configuration;
using System.Net.NetworkInformation;

namespace IvisMaui.Data
{
    class DataService
    {

        public SqlConnection con;
        private void connection()
        {
            con = new SqlConnection(Global.ConnectionString);
        }


        public bool PingSuccess()
        {
            try
            {
                var connstringArray = Global.ConnectionString.Split(';');
                var addressArray = connstringArray[0].Split('=');
                string address = "";
                if (addressArray[1].Contains(","))
                {
                    var ipaddressArray = addressArray[1].Split(',');
                    address = ipaddressArray[0];
                }
                else
                {
                    address = addressArray[1];
                }
                //var address = ipaddressArray[0];
                Ping netMon = new Ping();
                PingReply reply = netMon.Send(address, 4);
                if (reply != null)
                {
                    return true;
                    //ProcessResponse(response);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsServerConnected()
        {
            using (var l_oConnection = new SqlConnection(Global.ConnectionString))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        public string executeNonQuery(string query, DynamicParameters param)
        {
            try
            {
                if (Global.Connected)
                //if (PingSuccess())
                {
                    connection();
                    con.Open();
                    con.Execute(query, param, commandType: CommandType.StoredProcedure);
                    con.Close();
                    return "1";
                }
                return "0";
            }
            catch (Exception )
            {
                return "0";
            }
        }

        public string returnScalar(string query, DynamicParameters param)
        {
            try
            {
                string valor = "";
                connection();
                con.Open();
                valor = con.ExecuteScalar<string>(query, param, commandType: CommandType.StoredProcedure);
                con.Close();
                return valor;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string returnNumericValue(string query, DynamicParameters param)
        {
            try
            {
                string valor = "";
                param.Add("@output", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                // Getting Return value   
                connection();
                con.Open();
                valor = con.ExecuteScalar<string>(query, param, commandType: CommandType.StoredProcedure);
                con.Close();
                return valor;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
