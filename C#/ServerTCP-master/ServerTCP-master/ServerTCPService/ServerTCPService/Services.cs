using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerTCPService.Asset;

namespace ServerTCPService
{
    public static class Services
    {
        public static void SaveData(string data)
        {

            string query = "";
            object cmm = null;

            try
            {
                //var test = Properties.Settings.Default.__connection;
                //using (SqlConnection con = new SqlConnection(test))
                //{

                //    string aquery = "INSERT INTO [dbo].[TruckLocations]" +
                //                    "([TruckId],[Latitude],[Longitiude])" +
                //                    "VALUES(19,'from clinet','" + data.ToString() + "')";

                //    con.Open();
                //    SqlCommand aCommand = new SqlCommand(aquery, con);
                //    cmm = aCommand.ExecuteScalar();
                //    SqlCommand command = new SqlCommand(query, con);
                //    command.ExecuteNonQuery();
                //}
                using (Post_DbEntities db = new Post_DbEntities())
                {
                    db.TruckLocations.Add(new TruckLocation()
                    {
                        Latitude = "From client",
                        TruckId = 19,
                        Longitiude = data
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
