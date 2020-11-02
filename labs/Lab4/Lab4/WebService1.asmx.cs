using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Lab4
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    /*[WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]*/
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        readonly string path = @"workstation id=Transport.mssql.somee.com;packet size=4096;user id=AlexExo_SQLLogin_1;pwd=4bt6rs76op;data source=Transport.mssql.somee.com;persist security info=False;initial catalog=Transport";

        [WebMethod]

        public Dictionary<int, List<object>> GetData()
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT id, (SELECT num from Autos where id = Services.auto_id) as NumOfAuto, " +
                "(SELECT producer from Autos where id = Services.auto_id) as Producer, " +
                "(SELECT model from Autos where id = Services.auto_id) as Model, " +
                "(SELECT worktype from Works where id = Services.work_id) as Work, " +
                "(SELECT cost from Works where id = Services.work_id) as Cost, " +
                "(SELECT maxworktime from Works where id = Services.work_id) as MaxWorkTime, " +
                "servicetime, servicedate FROM Services", connection);

            int result = command.ExecuteNonQuery();

            Dictionary<int, List<object>> services = new Dictionary<int, List<object>>();
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                int i = 0;
                while (reader.Read())
                {
                    services.Add(i++, new List<object>
                    {
                        reader["id"].ToString(),
                        reader["NumOfAuto"].ToString(),
                        reader["Producer"].ToString(),
                        reader["Model"].ToString(),
                        reader["Work"].ToString(),
                        reader["Cost"].ToString(),
                        reader["MaxWorkTime"].ToString(),
                        reader["servicetime"].ToString(),
                        reader["servicedate"].ToString(),
                    }); ;
                }
            }

            connection.Close();

            return services;
        }

        [WebMethod]
        public ListItemCollection GetAutos()
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Autos", connection);

            int result = command.ExecuteNonQuery();

            ListItemCollection autos = new ListItemCollection();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    autos.Add(new ListItem(reader["num"].ToString(), reader["num"].ToString()));
                }
            }

            connection.Close();

            return autos;
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            SqlCommand command = new SqlCommand($"DELETE FROM Services where id = '{id}'", connection);

            int result = command.ExecuteNonQuery();

            connection.Close();
        }
        public ListItemCollection GetWorks()
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Works", connection);

            int result = command.ExecuteNonQuery();

            ListItemCollection works = new ListItemCollection();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    works.Add(new ListItem(reader["worktype"].ToString(), reader["worktype"].ToString()));
                }
            }

            connection.Close();

            return works;
        }

        public void AddNewService(string autoNum, string work, string servicetime, string workdate)
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            SqlCommand command = new SqlCommand($"INSERT INTO Services (auto_id, work_id, servicetime, servicedate) VALUES " +
                $"((select id from autos where num = '{autoNum}'), " +
                $"(select id from works where worktype = '{work}'), " +
                $"'{servicetime}', '{workdate}')", connection);

            int result = command.ExecuteNonQuery();

            connection.Close();
        }

        public Dictionary<int, List<object>> GetDataBetween(string from, string end)
        {
            SqlConnection connection = new SqlConnection(path);
            connection.Open();

            SqlCommand command = new SqlCommand($"SELECT id, (SELECT num from Autos where id = Services.auto_id) as NumOfAuto, " +
                $"(SELECT producer from Autos where id = Services.auto_id) as Producer, " +
                $"(SELECT model from Autos where id = Services.auto_id) as Model, " +
                $"(SELECT worktype from Works where id = Services.work_id) as Work, " +
                $"(SELECT cost from Works where id = Services.work_id) as Cost, " +
                $"(SELECT maxworktime from Works where id = Services.work_id) as MaxWorkTime, " +
                $"servicetime, servicedate FROM Services where servicedate BETWEEN '{from}' AND '{end}'", connection);

            int result = command.ExecuteNonQuery();

            Dictionary<int, List<object>> services = new Dictionary<int, List<object>>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                int i = 0;
                while (reader.Read())
                {
                    services.Add(i++, new List<object>
                    {
                        reader["id"].ToString(),
                        reader["NumOfAuto"].ToString(),
                        reader["Producer"].ToString(),
                        reader["Model"].ToString(),
                        reader["Work"].ToString(),
                        reader["Cost"].ToString(),
                        reader["MaxWorkTime"].ToString(),
                        reader["servicetime"].ToString(),
                        reader["servicedate"].ToString(),
                    }); ;
                }
            }

            connection.Close();

            return services;
        }
    }
}
